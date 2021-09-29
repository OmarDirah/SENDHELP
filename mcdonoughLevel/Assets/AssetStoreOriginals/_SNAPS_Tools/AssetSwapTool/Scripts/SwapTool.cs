using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditor.Compilation;
using System.Text.RegularExpressions;

namespace SNAP
{

    public class SwapTool : EditorWindow
    {

        static SnapRP currentRP;


        public static string PrefabPath;
        public static string PrefabPostfix;

        public static string PrefabHDPath;

        static bool groupEnabled;

        static string selectedRootPath;
        static string selectedHDRootPath;


        private bool ValidateToolSettings()
        {

            if (PrefabPath.Trim() == string.Empty || PrefabPostfix.Trim() == string.Empty || PrefabHDPath.Trim() == string.Empty)
                return false;

            if (!Directory.Exists(PrefabPath))
                return false;

            if (!Directory.Exists(PrefabHDPath))
                return false;

            string[] prefabFiles = Directory.GetFiles(PrefabPath, "*_" + PrefabPostfix.Trim() + ".prefab", SearchOption.AllDirectories);

            if (prefabFiles.Length == 0)
                return false;

            string[] prefabHDFiles = Directory.GetFiles(PrefabHDPath, "*_" + PrefabPostfix.Trim() + ".prefab", SearchOption.AllDirectories);

            if (prefabHDFiles.Length == 0)
                return false;

            return true;
        }

        public static string GetPrefabPath(Object asset)
        {
            string AssetPath = string.Empty;

            Object targetObj = PrefabUtility.GetCorrespondingObjectFromSource<Object>(asset);

            AssetPath = AssetDatabase.GetAssetPath(targetObj);

            if (AssetPath == string.Empty)
                Debug.LogWarning("Cound not find AssetPath : " + asset.ToString());

            return AssetPath;
        }

        public static string GetOriginalPrefabPath(Object asset)
        {
            string AssetPath = string.Empty;

            Object targetObj = PrefabUtility.GetCorrespondingObjectFromOriginalSource<Object>(asset);

            AssetPath = AssetDatabase.GetAssetPath(targetObj);

            if (AssetPath == string.Empty)
                Debug.LogWarning("Cound not find AssetPath : " + asset.ToString());

            return AssetPath;
        }


        static GameObject LoadPrefabByAssetPath(string TargetPath)
        {
            Object LoadedAsset = AssetDatabase.LoadAssetAtPath(TargetPath, typeof(GameObject));

            Object InstantiateObj = PrefabUtility.InstantiatePrefab(LoadedAsset);

            return ((GameObject)InstantiateObj);
        }

        static bool SwapGameObject(GameObject sourceGo, GameObject targetGo)
        {

            GameObject genGameObj;

            PrefabAssetType pref = PrefabUtility.GetPrefabAssetType(targetGo);

            if (pref == PrefabAssetType.Regular || pref == PrefabAssetType.Model)
            {
                genGameObj = (GameObject)PrefabUtility.InstantiatePrefab(targetGo);
            }
            else
            {
                genGameObj = (GameObject)Editor.Instantiate(targetGo);
            }

            Transform gTransform = genGameObj.transform;

            gTransform.parent = sourceGo.transform.parent;
            genGameObj.name = targetGo.name;

            gTransform.localPosition = sourceGo.transform.localPosition;
            gTransform.localScale = sourceGo.transform.localScale;
            gTransform.localRotation = sourceGo.transform.localRotation;

            DestroyImmediate(sourceGo);

            return true;
        }

        static bool SwapGameObjectByTargetPath(GameObject sourceGo, string targetPath)
        {
            try
            {

                GameObject genGameObj = LoadPrefabByAssetPath(targetPath);

                if (genGameObj == null)
                    return false;

                Transform gTransform = genGameObj.transform;

                gTransform.parent = sourceGo.transform.parent;
                gTransform.name = sourceGo.name;

                gTransform.localPosition = sourceGo.transform.localPosition;
                gTransform.localScale = sourceGo.transform.localScale;
                gTransform.localRotation = sourceGo.transform.localRotation;
                gTransform.localEulerAngles = sourceGo.transform.localEulerAngles;

                gTransform.position = sourceGo.transform.position;

                DestroyImmediate(sourceGo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        static void ExchangeAssetToSnap()
        {
            Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

            if (transforms.Length == 0)
            {
                EditorUtility.DisplayDialog("No selections in the scene", "Need to select at least one object.", "Ok");
                return;
            }

            Dictionary<string, string> SnapInfo = new Dictionary<string, string>();
            SnapInfo.Clear();

            SnapInfo = GetSnapMatchingTable(PrefabPath);


            foreach (Transform t in transforms)
            {

                string FoundPrefabPath = GetPrefabPath(t.gameObject);

                if (FoundPrefabPath.ToLower().Contains(PrefabPath.Replace(Application.dataPath, string.Empty).ToLower()))
                    return;

                FoundPrefabPath = Path.GetFileNameWithoutExtension(FoundPrefabPath).ToLower();


                


                if (SnapInfo.ContainsKey(FoundPrefabPath))
                {
                    string targetPrefab = string.Empty;
                    if (SnapInfo.TryGetValue(FoundPrefabPath, out targetPrefab))
                    {
                        bool swapResult = SwapGameObjectByTargetPath(t.gameObject, targetPrefab);

                        if (swapResult == false)
                            Debug.LogWarning(string.Format("Could not swap the object : {0}", t.name));
                    }

                }
                else
                {
                    Debug.LogWarning(string.Format("Could not find the matchable object : {0}", t.name));
                    
                }


            }
        }


        static bool ReExchangeSnapToObject()
        {
            bool swapResult = false;

            Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

            if (transforms.Length == 0)
            {
                EditorUtility.DisplayDialog("No selections in the scene", "Need to select at least one object.", "Ok");
                return false;
            }

            Dictionary<string, string> ObjInfo = new Dictionary<string, string>();
            ObjInfo.Clear();

            ObjInfo = GetObjectMatchingTable(PrefabPath);


            foreach (Transform t in transforms)
            {

                string FoundPrefabPath = GetPrefabPath(t.gameObject);

                if (!FoundPrefabPath.ToLower().Contains(PrefabPath.Replace(Application.dataPath, string.Empty).ToLower()))
                    return false;

                FoundPrefabPath = Path.GetFileNameWithoutExtension(FoundPrefabPath).ToLower();


                if (ObjInfo.ContainsKey(FoundPrefabPath))
                {
                    string targetPrefab = string.Empty;
                    if (ObjInfo.TryGetValue(FoundPrefabPath, out targetPrefab))
                    {
                        swapResult = SwapGameObjectByTargetPath(t.gameObject, targetPrefab);

                        if (swapResult == false)
                            Debug.LogWarning(string.Format("Could not swap the object : {0}", t.name));
                    }

                }
            }

            return swapResult;
        }


        static void ExchangeSnapToObject()
        {
            
            Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

            if (transforms.Length == 0)
            {
                EditorUtility.DisplayDialog("No selections in the scene", "Need to select at least one object.", "Ok");
                return;
            }

            Dictionary<string, string> ObjInfo = new Dictionary<string, string>();
            ObjInfo.Clear();

            ObjInfo = GetObjectMatchingTable(PrefabPath);


            foreach (Transform t in transforms)
            {

                string FoundPrefabPath = GetPrefabPath(t.gameObject);

                if (!FoundPrefabPath.ToLower().Contains( PrefabPath.Replace(Application.dataPath, string.Empty).ToLower() ))
                    return;

                FoundPrefabPath = Path.GetFileNameWithoutExtension(FoundPrefabPath).ToLower();


                if (ObjInfo.ContainsKey(FoundPrefabPath))
                {
                    string targetPrefab = string.Empty;
                    if (ObjInfo.TryGetValue(FoundPrefabPath, out targetPrefab))
                    {
                        bool swapResult = SwapGameObjectByTargetPath(t.gameObject, targetPrefab);

                        if (swapResult == false)
                            Debug.LogWarning(string.Format("Could not swap the object : {0}", t.name));
                    }

                }
                else
                {

                    Selection.activeObject = t;

                    Debug.LogWarning(string.Format("Could not find the matchable object : {0}", t.name));

                    

                    Regex reg = new Regex(@"_snaps[0-9][0-9][0-9]$");

                    
                    string pPath = GetPrefabPath(t.gameObject).ToLower();


                    Undo.RegisterCompleteObjectUndo(t.gameObject, "Before unpacking");
                    

                    UnpackNestedPrefab.UnpackSelectedPrefab(t.gameObject);
                    

                    Transform[] childTransforms = t.gameObject.GetComponentsInChildren<Transform>();

                    int successCount = 0;

                    foreach (Transform childTransform in childTransforms)
                    {
                        if (childTransform == null)
                            continue;

                        if (childTransform.gameObject == null)
                            continue;

                        if (PrefabUtility.GetPrefabAssetType(childTransform.gameObject) != PrefabAssetType.Regular)
                            continue;

                        Selection.activeObject = childTransform.gameObject;

                        if (ReExchangeSnapToObject())
                            successCount++;
                        
                    }
                    
                    if (successCount < 1)
                    {
                        Undo.PerformUndo();
                        //Undo.ClearUndo(t.gameObject);
                        continue;
                    }
                    
                    string GenSnapsHDPath = UnpackNestedPrefab.CreateGenSnapsHDFolder();
                    string GenSnapsPrototypePath = UnpackNestedPrefab.CreateGenSnapsPrototypeFolder();


                    string GenSnapsName = string.Empty;

                    if (reg.IsMatch(FoundPrefabPath))
                    {
                        GenSnapsName = string.Format("{0}/{1}.prefab", GenSnapsHDPath, FoundPrefabPath);
                    }
                    else
                    {
                        GenSnapsName = string.Format("{0}/{1}_{2}.prefab", GenSnapsHDPath, FoundPrefabPath, PrefabPostfix);
                    }

                    
                    Object GenPrefab = PrefabUtility.SaveAsPrefabAsset(t.gameObject, GenSnapsName);
                    
                    
                    SwapGameObjectByTargetPath(t.gameObject, GenSnapsName);



                    reg = new Regex(@"_snaps[0-9][0-9][0-9].prefab$");

                    if (pPath.Contains(UnpackNestedPrefab.PrefabRoot.ToLower()))
                    {
                        if (!reg.IsMatch(pPath))
                        {
                            AssetDatabase.RenameAsset(pPath, Path.GetFileNameWithoutExtension(pPath) + "_" + PrefabPostfix);
                        }
                        
                    }
                    else
                    {
                        if (!reg.IsMatch(pPath))
                        {
                            AssetDatabase.MoveAsset(pPath, GenSnapsPrototypePath + "/" + Path.GetFileNameWithoutExtension(pPath) + "_" + PrefabPostfix + ".prefab");
                        }
                        else
                        {
                            AssetDatabase.MoveAsset(pPath, GenSnapsPrototypePath + "/" + Path.GetFileName(pPath));
                        }
                    }


                }
            }

            Undo.ClearAll();

        }



        static void BulkExchangeSnapToHD()
        {
            
            Transform[] sceneTransforms = GameObject.FindObjectsOfType<Transform>();

            ArrayList targetTransform = new ArrayList();

            foreach(Transform sceneTransform in sceneTransforms)
            {
                GameObject parentPrefab = PrefabUtility.GetOutermostPrefabInstanceRoot(sceneTransform.gameObject);

                if (PrefabUtility.GetPrefabAssetType(sceneTransform) == PrefabAssetType.Regular)
                {
                    if (!targetTransform.Contains(parentPrefab))
                        targetTransform.Add(parentPrefab);
                    
                }
           
            }
            
            for(int i=0;i<targetTransform.Count;i++)
            {
                EditorUtility.DisplayProgressBar("Switching prefabs...", string.Format("{0}/{1} prefabs switched.", (i).ToString(), targetTransform.Count.ToString()), ((float)i / (float)targetTransform.Count ));
                Selection.activeObject = (GameObject)targetTransform[i];
                ExchangeSnapToObject();
            }

            EditorUtility.ClearProgressBar();

        }

        static void BulkExchangeHDToSnap()
        {
            Transform[] sceneTransforms = GameObject.FindObjectsOfType<Transform>();

            ArrayList targetTransform = new ArrayList();

            foreach (Transform sceneTransform in sceneTransforms)
            {
                GameObject parentPrefab = PrefabUtility.GetOutermostPrefabInstanceRoot(sceneTransform.gameObject);

                if (PrefabUtility.GetPrefabAssetType(sceneTransform) == PrefabAssetType.Variant || PrefabUtility.GetPrefabAssetType(sceneTransform) == PrefabAssetType.Regular)
                {
                    if (!targetTransform.Contains(parentPrefab))
                        targetTransform.Add(parentPrefab);
                }
            }

            for (int i = 0; i < targetTransform.Count; i++)
            {
                EditorUtility.DisplayProgressBar("Switching prefabs...", string.Format("{0}/{1} prefabs switched.", (i).ToString(), targetTransform.Count.ToString()), ((float)i / (float)targetTransform.Count));
                Selection.activeObject = (GameObject)targetTransform[i];
                ExchangeAssetToSnap();
            }

            EditorUtility.ClearProgressBar();
        }



        public static Dictionary<string, string> GetObjectMatchingTable(string searchingRootPath)
        {

            

            Dictionary<string, string> ObjDic = new Dictionary<string, string>();
            ObjDic.Clear();

            DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/../");

            string dataPath = dirInfo.FullName.Replace("\\", "/");

            string targetPrefabPath = searchingRootPath.ToLower().Replace(dataPath.ToLower(), string.Empty);


            //Debug.Log("@" + PrefabHDPath.ToLower().Replace(dataPath.ToLower(), string.Empty));

            
           
            string[] HDroots = new string[1];
            HDroots[0] = PrefabHDPath.ToLower().Replace(dataPath.ToLower(), string.Empty);

            if (AssetDatabase.IsValidFolder("Assets/GenSnapsHD"))
            {
                HDroots = new string[2];
                HDroots[0] = PrefabHDPath.ToLower().Replace(dataPath.ToLower(), string.Empty);
                HDroots[1] = "Assets/GenSnapsHD";
            }

            //string[] Assets = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });
            string[] Assets = AssetDatabase.FindAssets("t:Prefab", HDroots);




            foreach (string asset in Assets)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(asset);
                string assetNameWithoutEx = Path.GetFileNameWithoutExtension(assetPath).Trim();

                if (assetNameWithoutEx.ToLower().Contains(PrefabPostfix.ToLower()))
                {
                    if (!assetPath.ToLower().Contains(targetPrefabPath))
                    {
                        if (!ObjDic.ContainsKey(assetNameWithoutEx.ToLower()))
                            ObjDic.Add(assetNameWithoutEx.ToLower(), assetPath);
                    }
                }
            }

            return ObjDic;
        }

        public static Dictionary<string, string> GetSnapMatchingTable(string searchingRootPath)
        {

            DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/../");

            string ourput = dirInfo.FullName.Replace("\\", "/");

            string[] SnapAssets = AssetDatabase.FindAssets("t:Prefab", new[] { searchingRootPath.Replace(ourput, string.Empty) });


            Dictionary<string, string> Snap = new Dictionary<string, string>();
            Snap.Clear();


            foreach (string asset in SnapAssets)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(asset);
                string assetNameWithoutEx = Path.GetFileNameWithoutExtension(assetPath);


                if (assetNameWithoutEx.ToLower().Contains(PrefabPostfix.ToLower()))
                {
                    if (!Snap.ContainsKey(assetNameWithoutEx.ToLower()))
                    {
                        Snap.Add(assetNameWithoutEx.ToLower(), assetPath);
                    }

                }

            }

            return Snap;
        }


        [MenuItem("Snaps/Asset Swap Tool")]
        static void ShowSwapTool()
        {
            SwapTool SwapWindow = EditorWindow.GetWindow<SwapTool>();

            SwapWindow.titleContent.text = "Asset Swap Tool";

            Vector2 windowSize = new Vector2(400, 380);

            SwapWindow.minSize = windowSize;
            SwapWindow.maxSize = windowSize;

            SwapWindow.Show();

            if (Directory.Exists(PrefabPath))
            {
                groupEnabled = true;
            }
            else
                groupEnabled = false;

        }


        


        private string SearchFrequentPostfix(string targetPath)
        {
            string outString = string.Empty;

            Dictionary<string, int> postfixDic = new Dictionary<string, int>();

            if (!Directory.Exists(targetPath))
                return outString;

            string[] sPrefabs = Directory.GetFiles(targetPath, "*_snaps*.prefab", SearchOption.AllDirectories);

            foreach(string prefab in sPrefabs)
            {
                string[] tokens = Path.GetFileNameWithoutExtension(prefab).ToString().Split('_');
                string lasttoken = tokens[tokens.Length - 1].ToLower().Trim();


                if (!postfixDic.ContainsKey(lasttoken))
                {
                    postfixDic.Add(lasttoken, 1);
                }
                else
                {
                    postfixDic[lasttoken]++;
                }

            }

            var potfixList = postfixDic.ToList();
            potfixList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            if (potfixList.Count != 0)
                outString = potfixList[0].Key.Trim();

            return outString;
        }


        private bool FillOutSettingValues(string targetRootPath)
        {
            if (!Directory.Exists(targetRootPath))
            {
                EditorUtility.DisplayDialog("ERROR", string.Format("Could not find the Root Path : {0}", targetRootPath), "Ok");

                if (!Directory.Exists(PrefabPath))
                    groupEnabled = false;

                return false;
            }

            string frequentpostFix = SearchFrequentPostfix(targetRootPath);

            string[] foundSnapPrefabs = Directory.GetFiles(targetRootPath, "*_" + frequentpostFix + ".prefab", SearchOption.AllDirectories);

            if (foundSnapPrefabs.Length == 0)
            {
                EditorUtility.DisplayDialog("ERROR", string.Format("Could not find Snaps Prototype prefabs in \"{0}\"", targetRootPath), "Ok");
                groupEnabled = false;
                return false;
            }

            
            PrefabPath = targetRootPath;
            PrefabPostfix = frequentpostFix;

            if (Directory.Exists(PrefabHDPath))
                groupEnabled = true;

            Repaint();

            return true;

        }

        private bool FillOutHDSettingValues(string targetHDRootPath)
        {
            if (!Directory.Exists(targetHDRootPath))
            {
                EditorUtility.DisplayDialog("ERROR", string.Format("Could not find the Root Path : {0}", targetHDRootPath), "Ok");

                if (!Directory.Exists(PrefabHDPath))
                    groupEnabled = false;

                return false;
            }


            PrefabHDPath = targetHDRootPath;

            if (Directory.Exists(PrefabPath))
                groupEnabled = true;


            Repaint();

            return true;
        }
        private void OnGUI()
        {
            

            GUILayout.BeginVertical("Box");

            GUILayout.Label("Tool settings :", EditorStyles.boldLabel);
            PrefabPath = EditorGUILayout.TextField("Snaps Prototype PATH :", PrefabPath);
            PrefabHDPath = EditorGUILayout.TextField("Snaps Art / HD PATH :", PrefabHDPath);
            PrefabPostfix = EditorGUILayout.TextField("Snaps Prototype Postfix :", PrefabPostfix);

            
            
            if (GUILayout.Button(new GUIContent("Select a different project directory for Snaps Prototype prefabs.", "Set a root path to get a list of Snaps Prototype prefabs.")))
            {
                string targetPath = Path.Combine(Application.dataPath, @"AssetStoreOriginals\_SNAPS_PrototypingAssets");

                if (Directory.Exists(targetPath))
                {
                    selectedRootPath = EditorUtility.OpenFolderPanel("Select a root path containing Snaps Prototype prefabs", targetPath, string.Empty);
                }
                else
                    selectedRootPath = EditorUtility.OpenFolderPanel("Select a root path containing Snaps Prototype prefabs", Application.dataPath, string.Empty);

                if (FillOutSettingValues(selectedRootPath))
                {
                    if (SwapShader.CheckShaderSwap(out currentRP))
                    {
                        SwapShader.SwitchSnapPrototypeShader(currentRP, PrefabPath);
                    }

                }
            }


            if (GUILayout.Button(new GUIContent("Select a different project directory for Snaps Art / Art HD prefabs.", "Set a root path to get a list of Snaps Art / Art HD prefabs.")))
            {
                string targetPath = Application.dataPath;

                selectedHDRootPath = EditorUtility.OpenFolderPanel("Select a root path containing Snaps Prototype prefabs", Application.dataPath, string.Empty);

                if (FillOutHDSettingValues(selectedHDRootPath))
                {
                    if (SwapShader.CheckShaderSwap(out currentRP))
                    {
                        SwapShader.SwitchSnapPrototypeShader(currentRP, PrefabPath);
                    }

                }
            }


            GUILayout.EndVertical();  ////

            GUILayout.Label(string.Empty, EditorStyles.boldLabel);
            GUILayout.Label(string.Empty, EditorStyles.boldLabel);


            GUI.enabled = groupEnabled;


            GUILayout.BeginVertical("Box");

            GUILayout.Label("Swap prefabs by selections :", EditorStyles.boldLabel);

            if (GUILayout.Button(new GUIContent("Swap selections to Snaps Prototype prefabs", "Swap Snaps Art HD prefabs selected in the scene to corresponding Snaps Prototype prefabs.")))
            {
                if (ValidateToolSettings())
                {
                    ExchangeAssetToSnap();
                }
                else
                    groupEnabled = false;
            }

            if (GUILayout.Button(new GUIContent("Swap selections to Snaps Art HD prefabs", "Swap Snaps Prototype prefabs selected in the scene to corresponding Snaps Art HD prefabs.")))
            {
                if (ValidateToolSettings())
                {
                    SwapSnapToObject();
                }
                else
                    groupEnabled = false;
            }

            GUILayout.EndVertical();

            GUILayout.Label(string.Empty, EditorStyles.boldLabel);
            GUILayout.Label(string.Empty, EditorStyles.boldLabel);


            GUILayout.BeginVertical("Box");

            GUILayout.Label("Bulk Swaps in the Scene :", EditorStyles.boldLabel);

            if (GUILayout.Button(new GUIContent("Swap All to Snaps Prototype prefabs", "Swap all of Snaps Art HD prefabs in the scene to Snaps Prototype prefabs, if they have same names.")))
            {
                if (ValidateToolSettings())
                {
                    BulkExchangeHDToSnap();
                }
                else
                    groupEnabled = false;
            }

            if (GUILayout.Button(new GUIContent("Swap All to Snaps Art HD prefabs", "Swap all of Snaps Prototype prefabs in the scene to Snaps Art HD prefabs, if they have same names.")))
            {
                if (ValidateToolSettings())
                {
                    BulkExchangeSnapToHD();
                }
                else
                    groupEnabled = false;
            }

            GUILayout.EndVertical();

        }

        
        static void SwapObject()
        {
            ExchangeAssetToSnap();
        }


        static void SwapSnapToObject()
        {
            ExchangeSnapToObject();
        }


        

    }


}
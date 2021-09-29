using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;


namespace SNAP
{
    public class UnpackNestedPrefab : EditorWindow
    {

        public const string GenSnapsPrototypePath = "GenSnapsPrototype";
        public const string GenSnapsHDPath = "GenSnapsHD";

        
        public const string PrefabRoot = "Assets/AssetStoreOriginals/_SNAPS_PrototypingAssets";
        
        

        static bool IsSnapsPrototypePrefab(GameObject targetGo)
        {

            PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(targetGo);

            if (prefabType != PrefabAssetType.Regular && prefabType != PrefabAssetType.Variant)
                return false;


            Regex reg = new Regex(@"_snaps[0-9][0-9][0-9].prefab$");

            string PrefabPath = SwapTool.GetOriginalPrefabPath(targetGo).ToLower();



            if (PrefabPath.Contains(PrefabRoot.ToLower()) || PrefabPath.Contains(GenSnapsPrototypePath.ToLower()) )
            {
                if (reg.IsMatch(PrefabPath))
                    return true;
            }

            return false;
        }

        static bool IsSnapsHDPrefab(GameObject targetGo)
        {

            PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(targetGo);

            if (prefabType != PrefabAssetType.Regular && prefabType != PrefabAssetType.Variant)
                return false;

            if (IsSnapsPrototypePrefab(targetGo) == false)
            {

                Regex reg = new Regex(@"_snaps[0-9][0-9][0-9].prefab$");

                string PrefabPath = SwapTool.GetOriginalPrefabPath(targetGo).ToLower();

                string SnapsPrototypePath = SwapTool.PrefabPath.Replace(Application.dataPath, string.Empty).ToLower();

                if (reg.IsMatch(PrefabPath) && !PrefabPath.Contains(SnapsPrototypePath))
                    return true;
            }

            return false;
        }

        public static string CreateGenSnapsHDFolder()
        {
            string createdPath = string.Format("Assets/{0}", GenSnapsHDPath);

            if (!AssetDatabase.IsValidFolder(createdPath))
            {
                createdPath = AssetDatabase.GUIDToAssetPath( AssetDatabase.CreateFolder("Assets", GenSnapsHDPath) );
            }

            return createdPath;
        }

        public static string CreateGenSnapsPrototypeFolder()
        {
            string createdPath = string.Format("{0}/{1}", PrefabRoot, GenSnapsPrototypePath);

            if (!AssetDatabase.IsValidFolder(createdPath))
            {
                createdPath = AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder(PrefabRoot, GenSnapsPrototypePath));
            }

            return createdPath;
        }


        static bool SetUnpackPrefab(GameObject target)
        {
            if (target == null)
                return false;

            if (IsSnapsHDPrefab(target))
                return false;

            string PrefabPath = SwapTool.PrefabPath;

            Dictionary<string,string> ObjInfo = SwapTool.GetObjectMatchingTable(PrefabPath);

            string targetPrefabPath = Path.GetFileNameWithoutExtension( SwapTool.GetOriginalPrefabPath(target).ToLower() );

            if (ObjInfo.ContainsKey(targetPrefabPath))
                return false;


            PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(target);

            if (prefabType == PrefabAssetType.Regular || prefabType == PrefabAssetType.Variant)
            {
                if (target.transform.childCount == 0 && target.GetComponent<MeshRenderer>() == null)
                    return false;

                PrefabUtility.UnpackPrefabInstance(target, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            }

            return true;

        }


        public static void UnpackSelectedPrefab(GameObject currentObject)
        {
            Stack<GameObject> NestedGameObject = new Stack<GameObject>();

            NestedGameObject.Clear();


            if (SetUnpackPrefab(currentObject) == false)
                return;

            for (int i = 0; i < currentObject.transform.childCount; i++)
            {
                GameObject childGameObject = currentObject.transform.GetChild(i).gameObject;

                NestedGameObject.Push(currentObject.transform.GetChild(i).gameObject);
            }

            while (NestedGameObject.Count != 0)
            {
                GameObject gObj = NestedGameObject.Pop();

                if (SetUnpackPrefab(gObj) == false)
                    continue;

                for (int i = 0; i < gObj.transform.childCount; i++)
                {
                    NestedGameObject.Push(gObj.transform.GetChild(i).gameObject);
                }
            }

        }


    }
}
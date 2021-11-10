using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LockGameEvent))]
public class LockGameEventEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        LockGameEvent e = target as LockGameEvent;
        
        if (GUILayout.Button("RAISE"))
        {
            e.Raise();
        }
    }

}

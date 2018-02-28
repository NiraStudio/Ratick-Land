using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        GameManager t = (GameManager)target;
        if (GUILayout.Button("Restart"))
            t.FirstTimeChanges();
        base.OnInspectorGUI();

    }
}

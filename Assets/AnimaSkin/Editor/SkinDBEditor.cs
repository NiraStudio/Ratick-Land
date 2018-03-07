using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor
;
[CustomEditor(typeof(SkinDB))]
public class SkinDBEditor : Editor {

    public override void OnInspectorGUI()
    {
        SkinDB t = (SkinDB)target;
        if(GUILayout.Button("Save Skins"))
        {
            t.skins = new List<Skin>();
            foreach (var item in t.gameObject.GetComponents<Skin>())
            {
                t.skins.Add(item);
            }
        }
        base.OnInspectorGUI();
    }
    
}

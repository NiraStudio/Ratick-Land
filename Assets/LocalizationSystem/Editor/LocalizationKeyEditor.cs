using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Alpha.Localization;

[CustomEditor(typeof(LocalizedKeyText))]
public class LocalizationKeyEditor : Editor {
    static Vector2 AddWindowSize = new Vector2(500, 100);

    public override void OnInspectorGUI()
    {
        LocalizedKeyText t = (LocalizedKeyText)target;
        if(GUILayout.Button("Add Key"))
        {
            AddKeyEditor window = EditorWindow.GetWindow<AddKeyEditor>();
            window.minSize = AddWindowSize; window.maxSize = AddWindowSize;

            window.title = "Add Key";
            window.Show();
            window.Key = t.Key;
        }
        if (GUILayout.Button("Get English The Text"))
        {
            if (t.Key != null)
                t.textMesh.text = LocalizationManager.GiveData(t.Key, Language.EN);

            t.textMesh.font = GameObject.FindWithTag("GameManager").GetComponent<LocalizationManager>().ENFont;

        }
        if (GUILayout.Button("Get Persian The Text"))
        {
            if (t.Key != null)
                t.textMesh.text = LocalizationManager.GiveData(t.Key, Language.FA);


            t.textMesh.font = GameObject.FindWithTag("GameManager").GetComponent<LocalizationManager>().FAFont;
        }
        base.OnInspectorGUI();
    }
}

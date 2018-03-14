using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AdScript))]

public class AdScriptEditor : Editor {

    public override void OnInspectorGUI()
    {
        AdScript t = (AdScript)target;

        GUILayout.Label("Have AD = " + t.HaveAd, EditorStyles.boldLabel);
        GUILayout.Space(20);


        GUILayout.Label("AD Name:");
        t.AdName = EditorGUILayout.TextField(t.AdName);

        GUILayout.Label("AD ZoneID:");
        t.ZoneID = EditorGUILayout.TextField(t.ZoneID);


        GUILayout.Space(20);

        GUILayout.BeginVertical();
        for (int i = 0; i < t.adsType.Length; i++)
        {
            GUILayout.BeginHorizontal("Box");
            t.adsType[i].type = (RewardType)i;
            GUILayout.Label(t.adsType[i].type.ToString(),GUILayout.Width(100));

            t.adsType[i].Allow= EditorGUILayout.ToggleLeft("", t.adsType[i].Allow, GUILayout.Width(50));

            GUILayout.Label("Min Amount");
            t.adsType[i].Amount.m_Min = EditorGUILayout.IntField(t.adsType[i].Amount.m_Min);
            GUILayout.Label("Max Amount");
            t.adsType[i].Amount.m_Max = EditorGUILayout.IntField(t.adsType[i].Amount.m_Max);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();


        GUILayout.Space(20);


        GUILayout.Label("Extra Method");

        SerializedObject serializedObject = new SerializedObject(t);
        SerializedProperty serializedProperty = serializedObject.FindProperty("Extra");

        EditorGUILayout.PropertyField(serializedProperty, true);
        serializedObject.ApplyModifiedProperties();
    }

}

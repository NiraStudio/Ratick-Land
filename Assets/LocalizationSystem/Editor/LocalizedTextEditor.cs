using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Alpha.Localization
{
    public class LocalizedTextEditor : EditorWindow
    {
        public LocalizationData localizationData;
        Vector2 scrollPos;
        [MenuItem("AlphaTool/Localization")]
        static void InIt()
        {
            EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
        }

        void OnGUI()
        {
            if (localizationData != null)
            {
                scrollPos=GUILayout.BeginScrollView(scrollPos, "Box");
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");

                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();
                GUILayout.EndScrollView();
                if (GUILayout.Button("Save Data"))
                    SaveData();
            }
            if (GUILayout.Button("Load Data"))
                LoadData();
            if (GUILayout.Button("Create New Data"))
                CreateNewData();
        }


        void LoadData()
        {
            string filePath = EditorUtility.OpenFilePanel("Select Localization Data File", Application.streamingAssetsPath, "json");
            if (!string.IsNullOrEmpty(filePath))
            {

                string dataAsJson = File.ReadAllText(filePath);
                localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            }
        }

        void SaveData()
        {
            string filePath = EditorUtility.SaveFilePanel("Save Localization Data File", Application.streamingAssetsPath, "", "json");
            if (!string.IsNullOrEmpty(filePath))
            {

                string dataAsJson = JsonUtility.ToJson(localizationData);
                File.WriteAllText(filePath, dataAsJson);
            }
        }

        void CreateNewData()
        {
            localizationData = new LocalizationData();
        }
        void OnEnable()
        {
            if (!AssetDatabase.IsValidFolder("Assets/StreamingAssets"))
            {
                AssetDatabase.CreateFolder("Assets", "StreamingAssets");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Alpha.Localization
{
    public class LocalizedTextEditor : EditorWindow
    {
        public const string FOLDER_NAME = "DataBase";
        public const string FILE_NAME = "LocalizationDataBase.asset";
        public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;

        LocalizationDatabase dataBase;
        LocalizationDatabase tempDataBase;
        static Vector2 WindowSize = new Vector2(700, 300);
        static Vector2 AddWindowSize = new Vector2(500, 100);

        static Vector2 IconButtonSize = new Vector2(25, 25);
        Vector2  Scroll,Scroll2;
        Vector2[] Poses = new Vector2[System.Enum.GetValues(typeof(Language)).Length];
        string SearchName;


        [MenuItem("AlphaTool/Localization")]
        static void InIt()
        {
            LocalizedTextEditor window = EditorWindow.GetWindow<LocalizedTextEditor>();
            window.minSize = WindowSize; window.maxSize = WindowSize;

            window.title = "Localization";
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.BeginVertical();

            EditPart();

            GUILayout.BeginHorizontal("Box");
            SearchPart();
            AddKeyPart();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        void AddKeyPart()
        {
            if (GUILayout.Button("AddKey", GUILayout.ExpandWidth(true), GUILayout.Height(50)))
            {
                AddKeyEditor window = EditorWindow.GetWindow<AddKeyEditor>();
                window.minSize = AddWindowSize; window.maxSize = AddWindowSize;

                window.title = "Add Key";
                window.Show();
            }
        }

        void EditPart()
        {
            if (string.IsNullOrEmpty(SearchName))
            {
                Scroll = GUILayout.BeginScrollView(Scroll, "Box");


                GUILayout.BeginHorizontal();
                for (int i = 0; i < System.Enum.GetValues(typeof(Language)).Length; i++)
                {
                    Scroll2 = GUILayout.BeginScrollView(Scroll2, "Box");
                    EditorGUILayout.LabelField(((Language)i).ToString(), EditorStyles.boldLabel);



                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Keys");
                    GUILayout.Label("Values");
                    GUILayout.EndHorizontal();




                    GUILayout.BeginVertical();
                    foreach (var item in dataBase.Data((Language)i).Data.ToArray())
                    {
                        GUILayout.BeginHorizontal();

                        GUILayout.Label(item.Key + ":");
                        item.Value = EditorGUILayout.TextField(item.Value);

                        if (GUILayout.Button("X", GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
                        {
                            dataBase.RemoveItem(item.Key);
                        }
                        dataBase.setDirty();
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                    GUILayout.EndScrollView();

                }
                GUILayout.EndHorizontal();
                GUILayout.EndScrollView();
                dataBase.setDirty();


            }
            else
            {
               /* for (int i = 0; i < System.Enum.GetValues(typeof(Language)).Length; i++)
                {
                    Poses[i] = GUILayout.BeginScrollView(Poses[i], "Box");
                    EditorGUILayout.LabelField(((Language)i).ToString(), EditorStyles.boldLabel);



                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Keys");
                    GUILayout.Label("Values");
                    GUILayout.EndHorizontal();




                    GUILayout.BeginVertical();
                    foreach (var item in dataBase.Data((Language)i).Data.ToArray())
                    {
                        GUILayout.BeginHorizontal();

                        GUILayout.Label(item.Key + ":");
                        item.Value = EditorGUILayout.TextField(item.Value);

                        if (GUILayout.Button("X", GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
                        {
                            dataBase.RemoveItem(item.Key);
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                    GUILayout.EndScrollView();
                }
                */
            }
        }

        void SearchPart()
        {
            
        }

        void OnEnable()
        {
            dataBase = AssetDatabase.LoadAssetAtPath(FULL_PATH, typeof(LocalizationDatabase)) as LocalizationDatabase;

            if (dataBase == null)
            {
                if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                    AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

                dataBase = new LocalizationDatabase();
                AssetDatabase.CreateAsset(dataBase, FULL_PATH);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        /* public LocalizationData localizationData;
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
     }*/
    }

    public class AddKeyEditor : EditorWindow
    {

        public const string FOLDER_NAME = "DataBase";
        public const string FILE_NAME = "LocalizationDataBase.asset";
        public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;
        static Vector2 pos;

        LocalizationDatabase dataBase;
        static Vector2 IconButtonSize = new Vector2(25, 25);
        public string Key;
        string[] values = new string[System.Enum.GetValues(typeof(Language)).Length];
        void OnEnable()
        {
            dataBase = AssetDatabase.LoadAssetAtPath(FULL_PATH, typeof(LocalizationDatabase)) as LocalizationDatabase;

            if (dataBase == null)
            {
                if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                    AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

                dataBase = new LocalizationDatabase();
                AssetDatabase.CreateAsset(dataBase, FULL_PATH);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        void OnGUI()
        {


            GUILayout.BeginVertical();

            Key = EditorGUILayout.TextField("Key:", Key);


            for (int i = 0; i < System.Enum.GetValues(typeof(Language)).Length; i++)
            {
                values[i] = EditorGUILayout.TextField((Language)i + " Value", values[i]);
            }
            if (GUILayout.Button("Add", GUILayout.ExpandHeight(true)))
            {
                for (int i = 0; i < System.Enum.GetValues(typeof(Language)).Length; i++)
                {
                    if (!string.IsNullOrEmpty(Key))
                    {
                        dataBase.Add(this.Key, values[i], (Language)i);
                        dataBase.setDirty();
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("Key", "Key is empty ", "Yes"))
                        {
                            this.Close();
                            break;
                        }
                    }

                }
                this.Close();

            }


            GUILayout.EndVertical();

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkinDataCreatorAndEditor : EditorWindow {

    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "SkinDataBase.asset";
    public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;

    static Vector2 WindowSize = new Vector2(700, 300);
    static Vector2 IconButtonSize = new Vector2(100, 100);

    SkinDataBase database;

    Texture2D texture;
    SkinData temp;
    [MenuItem("AlphaTool/Create And Edit Skins")]
    public static void InIt()
    {
        SkinDataCreatorAndEditor window = EditorWindow.GetWindow<SkinDataCreatorAndEditor>();
        window.minSize = WindowSize; window.maxSize = WindowSize;

        window.title = "Create And Edit Skins";
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        Create();
        Edit();


        GUILayout.EndHorizontal();
    }
    void Create()
    {
        GUILayout.BeginVertical("Box");

        if (temp.Icon != null)
            texture = temp.Icon.texture;
        else
            texture = null;

        if (GUILayout.Button(texture, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
        }
        string commend = Event.current.commandName;
        if (commend == "ObjectSelectorClosed")
        {
            temp.Icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
        }




        GUILayout.EndVertical();
    }
    void Edit()
    {
        GUILayout.BeginVertical("Box");

        GUILayout.EndVertical();
    }
    
}

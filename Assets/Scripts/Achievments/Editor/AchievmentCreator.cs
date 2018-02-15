using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AchievmentCreator : EditorWindow
{

    public const string FileName = @"AchievmentDataBase.asset";
    public const string FolderName = @"DataBase";
    public const string FullPathName = @"Assets/" + FolderName + "/" + FileName;
    static Vector2 size = new Vector2(1000, 300);
    public Vector2 IconButtonSize = new Vector2(300, 300);
    public Vector2 CreateButtonSize = new Vector2(300, 50);
    AchievmentDataBase dB;
    Achievment temp;
    Texture2D t;

    [MenuItem("AchievmentSystem/AchievmentCreator")]
    public static void Init()
    {
        AchievmentCreator window = EditorWindow.GetWindow<AchievmentCreator>();
        window.minSize = size;
        window.maxSize = size;
        window.title = "Achievment Creator";
        window.Show();
    }
    void OnEnable()
    {
        dB = AssetDatabase.LoadAssetAtPath(FullPathName, typeof(AchievmentDataBase)) as AchievmentDataBase;
        if (dB == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FolderName))
                AssetDatabase.CreateFolder(@"Assets/DataBase", "Mission");

            dB = ScriptableObject.CreateInstance<AchievmentDataBase>();
            AssetDatabase.CreateAsset(dB, FullPathName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        temp = new Achievment();
    }
    void OnGUI()
    {

        GUILayout.BeginHorizontal();
        Icon();
        GUILayout.BeginVertical();
        UpVertical();
        DownVertical();

        if (GUILayout.Button("create", GUILayout.ExpandWidth(true), GUILayout.Height(CreateButtonSize.y)))
        {
            dB.dB.Add(temp);
            temp = new Achievment();
            t = null;
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

    }
    void MakePanel()
    {

    }
    void Icon()
    {
        if (temp.Icon != null)
        {
            t = temp.Icon.texture;
        }
        if (GUILayout.Button(t, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
        }
        string command = Event.current.commandName;
        if (command == "ObjectSelectorClosed")
        {
            Sprite sp = (Sprite)EditorGUIUtility.GetObjectPickerObject();
            if (sp != null)
            {
                temp.Icon = sp;
            }
        }

    }
    void UpVertical()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("name");
        temp.name = GUILayout.TextField(temp.name);
        GUILayout.Label("goal");
        temp.goal = EditorGUILayout.IntField(temp.goal);
        GUILayout.Label("Type");
        temp.achivType = (AchievmentType)EditorGUILayout.EnumPopup(temp.achivType);

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUILayout.Label("reward type");
        temp.reward.type = (ChestReward.Type)EditorGUILayout.EnumPopup(temp.reward.type);
        GUILayout.Label("reward amount");
        temp.reward.amount = EditorGUILayout.IntField(temp.reward.amount);
        GUILayout.Label("resetable");
        temp.resetAble = EditorGUILayout.Toggle(temp.resetAble);

        GUILayout.EndVertical();

    }
    void DownVertical()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label("Description");
        temp.description = GUILayout.TextArea(temp.description);

        GUILayout.EndHorizontal();
    }

}

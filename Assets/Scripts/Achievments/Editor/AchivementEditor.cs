using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AchivementEditor : EditorWindow
{

    public const string FileName = @"AchievmentDataBase.asset";
    public const string FolderName = @"DataBase";
    public const string FullPathName = @"Assets/" + FolderName + "/" + FileName;
    static Vector2 size = new Vector2(700, 250);
    public Vector2 IconButtonSize = new Vector2(100, 100);
    public Vector2 CreateButtonSize = new Vector2(300, 50);

    static Vector2 scroll;
    AchievementDataBase achievementDatabase;
    Achievement temp;
    Texture2D t;

    [MenuItem("AlphaTool/AchievmentSystem/AchivementEditor")]
    public static void Init()
    {
        AchivementEditor window = EditorWindow.GetWindow<AchivementEditor>();
        window.minSize = size;
        window.maxSize = size;
        window.title = "Achievment Editor";
        window.Show();
    }
    void OnEnable()
    {
        achievementDatabase = AssetDatabase.LoadAssetAtPath(FullPathName, typeof(AchievementDataBase)) as AchievementDataBase;
        if (achievementDatabase == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FolderName))
                AssetDatabase.CreateFolder(@"Assets/DataBase", "Mission");

            achievementDatabase = ScriptableObject.CreateInstance<AchievementDataBase>();
            AssetDatabase.CreateAsset(achievementDatabase, FullPathName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        temp = new Achievement();
    }

    void OnGUI()
    {
        scroll=GUILayout.BeginScrollView(scroll,"Box");
        for (int i = 0; i < achievementDatabase.dataBase.Count; i++)
        {
            temp = achievementDatabase.GiveByIndex(i);
            GUILayout.BeginVertical("Box");
            UpVertical();
            DownVertical();
            GUILayout.EndVertical();

        }
        GUILayout.EndScrollView();
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
        GUILayout.BeginHorizontal("Box");

        Icon();


        GUILayout.BeginVertical();





        //Title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Persian Title");
        temp.FatTitle = GUILayout.TextField(temp.FatTitle);
        GUILayout.Label("English Title");
        temp.EnTitle = GUILayout.TextField(temp.EnTitle);
        GUILayout.EndHorizontal();


        //Type And Goal
        GUILayout.BeginHorizontal();
        GUILayout.Label("Type");
        temp.achievementType = (AchievementType)EditorGUILayout.EnumPopup(temp.achievementType);
        GUILayout.Label("goal");
        temp.goalObject = EditorGUILayout.IntField(temp.goalObject);
        GUILayout.EndHorizontal();



        //Reward and Resetable
        GUILayout.BeginHorizontal();
        GUILayout.Label("reward type");
        temp.rewardType = (RewardType)EditorGUILayout.EnumPopup(temp.rewardType);
        GUILayout.Label("reward amount");
        temp.rewardAmount = EditorGUILayout.IntField(temp.rewardAmount);
        GUILayout.Label("resetable");
        temp.resetable = EditorGUILayout.Toggle(temp.resetable);
        GUILayout.EndHorizontal();


        GUILayout.EndVertical();



        GUILayout.EndHorizontal();




    }
    void DownVertical()
    {
        GUILayout.BeginVertical("Box");

        GUILayout.Label("Persian Description");
        temp.FaDes = GUILayout.TextArea(temp.FaDes);
        GUILayout.Label("English Description");
        temp.EnDes = GUILayout.TextArea(temp.EnDes);

        GUILayout.EndVertical();
    }
}

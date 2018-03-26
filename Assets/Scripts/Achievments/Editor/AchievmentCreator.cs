using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AchievmentCreator : EditorWindow
{

    public const string FileName = @"AchievmentDataBase.asset";
    public const string FolderName = @"DataBase";
    public const string FullPathName = @"Assets/" + FolderName + "/" + FileName;
    static Vector2 size = new Vector2(700, 250);
    public Vector2 IconButtonSize = new Vector2(100, 100);
    public Vector2 CreateButtonSize = new Vector2(300, 50);
    AchievementDataBase achievementDatabase;
    Achievement temp;
    Texture2D t;

    [MenuItem("AlphaTool/AchievmentSystem/AchievmentCreator")]
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


        GUILayout.BeginVertical();
        UpVertical();
        DownVertical();

        if (GUILayout.Button("create", GUILayout.ExpandWidth(true), GUILayout.Height(CreateButtonSize.y)))
        {
            temp.id = GiveID();
            achievementDatabase.AddAchievment(temp);
            temp = new Achievement();
            t = null;
        }
        GUILayout.EndVertical();

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

        if (temp.achievementType == AchievementType.Specific)
        {
            GUILayout.Label("Tag");
            temp.tag = GUILayout.TextField(temp.tag);
        }
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
    public string GiveID()
    {
        string answer = "";
        bool find = true;
        
        do
        {
            find = true;
            answer = "";

            for (int i = 0; i < 10; i++)
            {
                int a = Random.Range(1, 3);
                if (a == 1)
                {
                    answer += (char)Random.Range(48, 58);
                }
                else
                {
                    answer += (char)Random.Range(65, 90);
                }
            }

            foreach (var item in achievementDatabase.dataBase)
            {
                if(item.id==answer)
                {
                    find = false;
                    break;
                }
            }
        } while (find==false);

        return answer;
    }
}

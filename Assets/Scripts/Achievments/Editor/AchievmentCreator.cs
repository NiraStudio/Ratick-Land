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
    AchievementDataBase achievementDatabase;
    Achievement temp;
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

        GUILayout.BeginHorizontal();
        Icon();
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
        temp.achievementType  = (AchievementType)EditorGUILayout.EnumPopup(temp.achievementType );

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUILayout.Label("reward type");
        temp.reward.type = (ChestReward.Type)EditorGUILayout.EnumPopup(temp.reward.type);
        GUILayout.Label("reward amount");
        temp.reward.amount = EditorGUILayout.IntField(temp.reward.amount);
        GUILayout.Label("resetable");
        temp.resetable = EditorGUILayout.Toggle(temp.resetable);

        GUILayout.EndVertical();

    }
    void DownVertical()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label("Description");
        temp.description = GUILayout.TextArea(temp.description);

        GUILayout.EndHorizontal();
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

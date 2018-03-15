using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSystmeEditorEdit : EditorWindow
{
    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "CharacterDataBase.asset";
    public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;

    CharacterDataBase dataBase,db;
    static Vector2 WindowSize = new Vector2(1200, 500);
    static Vector2 IconButtonSize = new Vector2(75, 100);
    static Vector2 Scrollpos;
    static Vector2 CharacterBtnSize = new Vector2(75, 100);
    static Vector2 ButtonScroll, DetailScroll, AttributesScroll, UpgradesScroll;
    Texture2D ItemIcon;
    CharacterData temp=null;
    string searchName="";


    int aa = 0;

    [MenuItem("AlphaTool/Character System/Edit Character")]
    public static void InIt()
    {
        CharacterSystmeEditorEdit window = EditorWindow.GetWindow<CharacterSystmeEditorEdit>();
        window.minSize = WindowSize; window.maxSize = WindowSize;

        window.title = "Character Editor";
        window.Show();
    }

    void OnEnable()
    {
        dataBase = AssetDatabase.LoadAssetAtPath(FULL_PATH, typeof(CharacterDataBase)) as CharacterDataBase;

        if (dataBase == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

            dataBase = new CharacterDataBase();
            AssetDatabase.CreateAsset(dataBase, FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        Searching();

        if (temp != null)
        {
            GUILayout.BeginHorizontal();
            Edit();
            GUILayout.EndHorizontal();
        }

        GUILayout.EndHorizontal();
    }


    void Searching()
    {
        GUILayout.BeginVertical(GUILayout.Width(WindowSize.x/5));

        GUILayout.BeginVertical("Box", GUILayout.Height(WindowSize.y / 5));

        GUILayout.Label("Search Character name or ID:");
        searchName = GUILayout.TextField(searchName);
        GUILayout.Label("Character Count :" + dataBase.Length);
        GUILayout.EndVertical();


        ButtonScroll = GUILayout.BeginScrollView(ButtonScroll, "Box");

        db = new CharacterDataBase();
        if (string.IsNullOrEmpty(searchName))
            db = dataBase;
        else
            for (int i = 0; i < dataBase.Length; i++)
            {
                if (dataBase.GiveByIndex(i).characterName.Contains(searchName) || dataBase.GiveByIndex(i).id.ToString().Contains(searchName))
                    db.AddCharacter(dataBase.GiveByIndex(i));
            }



        //Making Buttons
        for (int i = 0; i < db.Length; i++)
        {
            GUILayout.BeginHorizontal("Box");
            if (db.GiveByIndex(i).icon != null)
                ItemIcon = db.GiveByIndex(i).icon.texture;
                
            if (GUILayout.Button(ItemIcon,GUILayout.Height(IconButtonSize.y), GUILayout.Width(IconButtonSize.x)))
            {
                temp = db.GiveByIndex(i);
            }

            GUILayout.BeginVertical();
            GUILayout.Label("Character Name: ",EditorStyles.boldLabel);
            GUILayout.Label(db.GiveByIndex(i).characterName);
            GUILayout.Label("Character Type: ", EditorStyles.boldLabel);
            GUILayout.Label( db.GiveByIndex(i).type.ToString());
            GUILayout.Label("Character ID: ", EditorStyles.boldLabel);
            GUILayout.Label( db.GiveByIndex(i).id.ToString());

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }


        GUILayout.EndScrollView();



        GUILayout.EndVertical();
    }

    void Edit()
    {



        #region Details

        GUILayout.BeginVertical();
        DetailScroll = GUILayout.BeginScrollView(DetailScroll, "Box");

        GUILayout.Label("Details", EditorStyles.boldLabel);


        //ICon Chooser Part
        GUILayout.BeginHorizontal();


        if (temp.icon != null)
            ItemIcon = temp.icon.texture;


        if (GUILayout.Button(ItemIcon, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
        }
        string commend = Event.current.commandName;
        if (commend == "ObjectSelectorClosed")
        {
            temp.icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
        }
        

        if (GUILayout.Button("Delete", GUILayout.Width(100), GUILayout.Height(40)))
        {
            if (EditorUtility.DisplayDialog("Delete Character", "Are you sure you want to delete " + temp.name + "?", "Yes", "No"))
            {
                AssetDatabase.DeleteAsset(@"Assets/Data/CharacterData/" + temp.name + ".asset");
                dataBase.RemoveCharacter(temp);
                temp = null;
                return;

            }
        };

        if (GUILayout.Button("Back", GUILayout.Width(100), GUILayout.Height(40)))
        {
            temp = null;
            return;
        };
        GUILayout.EndHorizontal();

        GUILayout.Label("Character Name:");
        temp.characterName = GUILayout.TextField(temp.characterName, GUILayout.Width(200));

        GUILayout.Label("Character Shape:");
        temp.prefab = EditorGUILayout.ObjectField(temp.prefab, typeof(GameObject), false) as GameObject;

        GUILayout.Label("Character Type:");
        temp.type = (CharacterData.Type)EditorGUILayout.EnumPopup(temp.type);

        temp.id = EditorGUILayout.IntField("ID:", temp.id);

        GUILayout.Label("Item Description:");
        temp.description = EditorGUILayout.TextArea(temp.description, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));


        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        #endregion




        #region Attributes


        GUILayout.BeginVertical();
        AttributesScroll = GUILayout.BeginScrollView(AttributesScroll, "Box");

        GUILayout.Label("Attributes", EditorStyles.boldLabel);


        temp.damage = EditorGUILayout.IntField("Damage:", temp.damage);
        temp.hitPoint = EditorGUILayout.FloatField("Hit Point:", temp.hitPoint);





        temp.attackSpeed = EditorGUILayout.FloatField("Attack Speed:", temp.attackSpeed);
        temp.speed = EditorGUILayout.FloatField("Speed:", temp.speed);







        temp.maxLevel = EditorGUILayout.IntField("Max Level:", temp.maxLevel);

        temp.attackRange = EditorGUILayout.FloatField("Attack range:", temp.attackRange);


        temp.upgradePrice.Amount = EditorGUILayout.IntField("Upgrade Price:", temp.upgradePrice.Amount, GUILayout.Width(300));
        temp.upgradePrice.type = (Currency.Type)EditorGUILayout.EnumPopup(temp.upgradePrice.type);



        temp.baseCardNeed = EditorGUILayout.IntField("Base Card Need:", temp.baseCardNeed, GUILayout.Width(300));

        temp.CardNeedIncrease = EditorGUILayout.IntField("Card Increase After Each Update:", temp.CardNeedIncrease, GUILayout.Width(300));


        GUILayout.EndScrollView();
        GUILayout.EndVertical();


        #endregion




        #region Upgrades
        GUILayout.BeginVertical();
        UpgradesScroll = GUILayout.BeginScrollView(UpgradesScroll, "Box");
        GUILayout.Label("Upgrades", EditorStyles.boldLabel);


        SerializedObject serializedObject = new SerializedObject(temp);
        SerializedProperty serializedProperty = serializedObject.FindProperty("UpgradesForEachLevel");

        EditorGUILayout.PropertyField(serializedProperty, true);
        serializedObject.ApplyModifiedProperties();


        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        #endregion


    }


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSystemEditorCreate : EditorWindow {

    
    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "CharacterDataBase.asset";
    public const string FULL_PATH = @"Assets/"+FOLDER_NAME+"/"+FILE_NAME;

    CharacterDataBase dataBase;
    static Vector2 WindowSize=new Vector2(1000, 500);
    static Vector2 IconButtonSize = new Vector2(75, 100);
    Texture2D ItemIcon;
    CharacterData temp;
    Vector2 DetailScroll, AttributesScroll, UpgradesScroll;

    [MenuItem("AlphaTool/Character System/Create Character")]
    public static void InIt()
    {
        CharacterSystemEditorCreate window = EditorWindow.GetWindow<CharacterSystemEditorCreate>();
        window.minSize = WindowSize; window.maxSize = WindowSize;
        
        window.title = "Character Creator";
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
        temp = new CharacterData();
        EditorUtility.SetDirty(temp);
    }

    void OnGUI()
    {
        if (GUI.changed)
        {
            EditorUtility.SetDirty(temp);

        }
        GUILayout.BeginVertical("Box");
        



        GUILayout.BeginHorizontal("Box");

        //DetailPart
        DetailPart();
        GUILayout.EndHorizontal();




        GUILayout.BeginHorizontal("Box",GUILayout.Height(WindowSize.y *(.5f/ 3)));

       //create BTN
        if(GUILayout.Button("Create Character",GUILayout.ExpandWidth(true),GUILayout.ExpandHeight(true))){

            CharacterData a = ScriptableObject.CreateInstance<CharacterData>();
            a.characterName = temp.characterName;
            a.prefab = temp.prefab;
            a.id = temp.id;
            a.icon = temp.icon;
            a.speed = temp.speed;
            a.type = temp.type;
            a.attackSpeed = temp.attackSpeed;
            a.hitPoint = temp.hitPoint;
            a.damage = temp.damage;
            a.description = temp.description;
            a.baseCardNeed = temp.baseCardNeed;
            a.CardNeedIncrease = temp.CardNeedIncrease;
            a.maxLevel = temp.maxLevel;
            a.attackRange = temp.attackRange;
            a.upgradePrice = temp.upgradePrice;
            a.UpgradesForEachLevel = temp.UpgradesForEachLevel;

            string path = @"Assets/Data/CharacterData/";
            if (!AssetDatabase.IsValidFolder(@"Assets/Data"))
                AssetDatabase.CreateFolder("Assets", "Data");

            if (!AssetDatabase.IsValidFolder(@"Assets/Data/" + "CharacterData"))
                AssetDatabase.CreateFolder(@"Assets/Data", "CharacterData");
            string b;
            if (a.characterName != null)
                b = temp.characterName;
            else
                b = "New character data";
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + b + ".asset");

            AssetDatabase.CreateAsset(a, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            dataBase.AddCharacter(a);
            temp = new CharacterData();
            EditorUtility.SetDirty(temp);
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    void DetailPart()
    {


        #region Details

        GUILayout.BeginVertical();
        DetailScroll = GUILayout.BeginScrollView(DetailScroll, "Box");

        GUILayout.Label("Details", EditorStyles.boldLabel);


        //ICon Chooser Part

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
        


        GUILayout.Label("Character Name:");
        temp.characterName = GUILayout.TextField(temp.characterName, GUILayout.Width(200));

        GUILayout.Label("Character Shape:");
        temp.prefab = EditorGUILayout.ObjectField(temp.prefab, typeof(GameObject),false) as GameObject;

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


        temp.damage.m_Min = EditorGUILayout.IntField("Damage Min:", temp.damage.m_Min);
        temp.damage.m_Max = EditorGUILayout.IntField("Damage Max:", temp.damage.m_Max);
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
  
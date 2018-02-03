using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSystemEditorCreate : EditorWindow {

    
    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "CharacterDataBase.asset";
    public const string FULL_PATH = @"Assets/"+FOLDER_NAME+"/"+FILE_NAME;

    CharacterDataBase dataBase;
    static Vector2 WindowSize=new Vector2(1000,500);
    static Vector2 IconButtonSize = new Vector2(50, 50);
    Texture2D ItemIcon;
    CharacterData temp;

    [MenuItem("Character System/Create Character")]
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
            a.buyPrice.Amount = temp.buyPrice.Amount;
            a.buyPrice.type = temp.buyPrice.type;

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
        GUILayout.BeginVertical();


        #region Icon & Detail

        GUILayout.BeginHorizontal("Box");

        #region Icon Part

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

        #endregion

        #region Detail Part

        GUILayout.BeginVertical();

        #region Name & Game Object

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Character Name:");
        temp.characterName = GUILayout.TextField(temp.characterName, GUILayout.Width(200));

        GUILayout.Label("Character Shape:");
        temp.prefab = EditorGUILayout.ObjectField(temp.prefab, typeof(GameObject)) as GameObject;

        GUILayout.Label("Character Type:");
        temp.type = (CharacterData.Type)EditorGUILayout.EnumPopup(temp.type);


        GUILayout.EndHorizontal();

        #endregion

        #region Damage , Hit Point & Type
        GUILayout.BeginHorizontal("Box");

        //Damage
        temp.damage.m_Min = EditorGUILayout.IntField("Damage Min:", temp.damage.m_Min);
        temp.damage.m_Max = EditorGUILayout.IntField("Damage Max:", temp.damage.m_Max);
        //HitPoint
        temp.hitPoint = EditorGUILayout.FloatField("Hit Point:", temp.hitPoint);



        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("Box");

        //attackSpeed
        temp.attackSpeed = EditorGUILayout.FloatField("Attack Speed:", temp.attackSpeed);
        //Speed
        temp.speed = EditorGUILayout.FloatField("Speed:", temp.speed);



        GUILayout.EndHorizontal();

        #endregion

        #region Max ,id ,attackrange

        GUILayout.BeginHorizontal("Box");

        //maxLevel
        temp.maxLevel = EditorGUILayout.IntField("Max Level:", temp.maxLevel);

        //Range
        temp.attackRange = EditorGUILayout.FloatField("Attack range:", temp.attackRange);

        //ID
        temp.id = EditorGUILayout.IntField("ID:", temp.id);
        GUILayout.EndHorizontal();




        GUILayout.EndVertical();

        #endregion




        GUILayout.EndHorizontal();


        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal("Box");


        
        temp.buyPrice.Amount = EditorGUILayout.IntField("Buy Price:", temp.buyPrice.Amount, GUILayout.Width(300));
        temp.buyPrice.type = (Currency.Type)EditorGUILayout.EnumPopup(temp.buyPrice.type);

        temp.upgradePrice.Amount = EditorGUILayout.IntField("Upgrade Price:", temp.upgradePrice.Amount, GUILayout.Width(300));
        temp.upgradePrice.type = (Currency.Type)EditorGUILayout.EnumPopup(temp.upgradePrice.type);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("Box");

        temp.upgrade.amount = EditorGUILayout.IntField("Upgrade Amount:", temp.upgrade.amount);
        temp.upgrade.type = (Upgrade.Type)EditorGUILayout.EnumPopup(temp.upgrade.type);

        GUILayout.EndHorizontal();

        GUILayout.Label("Item Description:");
        temp.description = EditorGUILayout.TextArea(temp.description, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

        GUILayout.EndVertical();

        #endregion
        GUILayout.EndVertical();
        #endregion

    }
}
  
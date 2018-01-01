﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSystmeEditorEdit : EditorWindow
{
    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "CharacterDataBase.asset";
    public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;

    CharacterDataBase dataBase,db;
    static Vector2 WindowSize = new Vector2(1100, 500);
    static Vector2 IconButtonSize = new Vector2(50, 50);
    static Vector2 Scrollpos;
    Texture2D ItemIcon;
    CharacterData temp;
    string searchName="";

    [MenuItem("Character System/Edit Character")]
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
        temp = new CharacterData();
        EditorUtility.SetDirty(temp);
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();

        

        EditScroll();


        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Search Character name or ID:");
        searchName = GUILayout.TextField(searchName, GUILayout.Width(500));
        GUILayout.Label("Character Count :"+dataBase.Length);

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }





    void EditScroll()
    {
        db = new CharacterDataBase();
        if (searchName == null)
           db=dataBase;
       else
            for (int i = 0; i < dataBase.Length; i++)
            {
                if (dataBase.GiveByIndex(i).characterName.Contains(searchName) || dataBase.GiveByIndex(i).id.ToString().Contains(searchName))
                    db.AddCharacter(dataBase.GiveByIndex(i));
            }
        Scrollpos = GUILayout.BeginScrollView(Scrollpos, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        for (int i = 0; i < db.Length; i++)
        {
            temp = db.GiveByIndex(i);

            GUILayout.BeginVertical("Box");

             
            GUILayout.BeginHorizontal();

            #region icon
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


            #region Name ,Shape ,Dmg ,Hp  ,X &

            GUILayout.BeginVertical();

            #region Name ,Shape
            GUILayout.BeginHorizontal();

            GUILayout.Label("Character Name:", GUILayout.Width(250));
            temp.characterName = GUILayout.TextField(temp.characterName, GUILayout.Width(250));

            GUILayout.Label("Character Shape:", GUILayout.Width(200));
            temp.prefab = EditorGUILayout.ObjectField(temp.prefab, typeof(GameObject), GUILayout.Width(200)) as GameObject;

            

            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                if (EditorUtility.DisplayDialog("Delete Character", "Are you sure you want to delete " + temp.name + "?", "Yes", "No"))
                {
                    AssetDatabase.DeleteAsset(@"Assets/Data/CharacterData/" + temp.name + ".asset");
                    dataBase.RemoveCharacter(temp);
                }
            };

            GUILayout.EndHorizontal();
            #endregion

            #region dmg ,hp, Speed
            GUILayout.BeginHorizontal();

            //Damage
            temp.damage = EditorGUILayout.FloatField("Damage:", temp.damage, GUILayout.Width(300));
            //HitPoint
            temp.hitPoint = EditorGUILayout.FloatField("Hit Point:", temp.hitPoint, GUILayout.Width(300));

            //attackSpeed
            temp.speed = EditorGUILayout.FloatField("Speed:", temp.speed, GUILayout.Width(300));
            

            GUILayout.EndHorizontal();
            #endregion


            #region attackSpeed,ID
            GUILayout.BeginHorizontal();


            //ID
            temp.id = EditorGUILayout.IntField("ID:", temp.id, GUILayout.Width(300));

            //Speed
            temp.attackSpeed = EditorGUILayout.FloatField("Attack Speed:", temp.attackSpeed, GUILayout.Width(300));

            GUILayout.EndHorizontal();


            GUILayout.EndVertical();
            #endregion

            #endregion


            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal("Box");
          
            GUILayout.EndHorizontal();

            GUILayout.Label("Character Description:");
            temp.description = EditorGUILayout.TextArea(temp.description, GUILayout.Height(70), GUILayout.ExpandWidth(true));


            GUILayout.EndVertical();
        }
        GUILayout.EndScrollView();

    }
}

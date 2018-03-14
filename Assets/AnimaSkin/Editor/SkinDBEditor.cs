using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor
;
public class SkinDBEditor : Editor
{


    [MenuItem("AlphaTool/ReLoadSkins")]
    public static void InIt()
    {
        string FOLDER_NAME = "DataBase";
        string FILE_NAME = "SkinDataBase.asset";
        string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;

        SkinDataBase database;
        CharacterDataBase characterDataBase;

        Skin[] skins = new Skin[0];
        SkinData a = new SkinData();

        #region OnEnable

        database = AssetDatabase.LoadAssetAtPath(FULL_PATH, typeof(SkinDataBase)) as SkinDataBase;

        if (database == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

            database = new SkinDataBase();
            AssetDatabase.CreateAsset(database, FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();


        }


        characterDataBase = AssetDatabase.LoadAssetAtPath(@"Assets/" + FOLDER_NAME + "/" + "CharacterDatabase.asset", typeof(CharacterDataBase)) as CharacterDataBase;

        if (characterDataBase == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

            characterDataBase = new CharacterDataBase();
            AssetDatabase.CreateAsset(characterDataBase, FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion


        database.CleanDataBase();
        foreach (var item in characterDataBase.DataBase.ToArray())
        {
            skins = new Skin[0];
            if (item.prefab != null&& item.prefab.GetComponent<SkinManager>())
                skins = item.prefab.GetComponent<SkinManager>().skinHolder.GetComponents<Skin>();

            
            for (int i = 0; i < skins.Length; i++)
            {
                a = new SkinData();
                a.CharacterID = item.id;
                a.Icon = skins[i].Icon;
                a.SkinName = skins[i].skinName;
                a.Price = skins[i].Price;
                database.AddSkin(a);
            }
        }
        Selection.activeObject = database;


    }



}

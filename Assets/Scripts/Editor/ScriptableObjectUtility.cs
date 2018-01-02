using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void CreateAsset<T>(string FolderName ) where T : ScriptableObject
    {

           T asset = ScriptableObject.CreateInstance<T>();


        string path = @"Assets/Data/" + FolderName;
        if (!AssetDatabase.IsValidFolder(@"Assets/Data"))
            AssetDatabase.CreateFolder("Assets", "Data");

        if (!AssetDatabase.IsValidFolder(@"Assets/Data/" + FolderName))
            AssetDatabase.CreateFolder(@"Assets/Data", FolderName);
        string a;
        
            a = "/New " + typeof(T).ToString();
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + a + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetCreator : MonoBehaviour {

    [MenuItem("Data Creator/Create Character Data")]
    public static void CreateCharacterData()
    {
        ScriptableObjectUtility.CreateAsset<CharacterData>("CharacterData", null,null);
    }
   
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetCreator : MonoBehaviour {

    [MenuItem("Data Creator/Create Enemy Data")]
    public static void CreateCharacterData()
    {
        ScriptableObjectUtility.CreateAsset<EnemyData>("EnemyData");
    }
    
   


}

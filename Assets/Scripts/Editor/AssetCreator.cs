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
    [MenuItem("Data Creator/Create Boss Data")]
    public static void CreateBossData()
    {
        ScriptableObjectUtility.CreateAsset<BossData>("BossData");
    }

   /* [MenuItem("Data Creator/Create test Data")]
    public static void CreatetestData()
    {
        ScriptableObjectUtility.CreateAsset<BossData>("BossData");
    }*/




}

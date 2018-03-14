using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetCreator : MonoBehaviour {

    [MenuItem("AlphaTool/Data Creator/Create Enemy Data")]
    public static void CreateCharacterData()
    {
        ScriptableObjectUtility.CreateAsset<EnemyData>("EnemyData");
    }
    [MenuItem("AlphaTool/Data Creator/Create Boss Data")]
    public static void CreateBossData()
    {
        ScriptableObjectUtility.CreateAsset<BossData>("BossData");
    }

    [MenuItem("AlphaTool/Data Creator/Create test Data")]
    public static void CreatetestData()
    {
        ScriptableObjectUtility.CreateAsset<StringDataBase>("");
    }




}

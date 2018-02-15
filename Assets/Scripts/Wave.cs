﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wave : MonoBehaviour {

    public enum EnemyType
    {
        MeleeNormal,MeleeFast,RangeShort,RangeLong,Tank,EliteMeleeNormal,EliteMeleeFast,EliteRangeShort, EliteRangeLong, EliteTank
    }
    [System.Serializable]
    public class WaveEnemy
    {
        public EnemyType type;
        public GameObject Enemy;

    }


    public List<GameObject> currentEnemies = new List<GameObject>();
    public List<WaveEnemy> Enemies = new List<WaveEnemy>();
    [Header("MeleeNormal = 0,MeleeFast = 1,RangeShort = 2,RangeLong = 3,Tank = 4,EliteMeleeNormal = 5 ")]
    [Header("EliteMeleeFast = 6,EliteRangeShort = 7, EliteRangeLong = 8, EliteTank = 9")]
    public UnityEvent[] events;

    public LevelController LC;

    void Start()
    {
        LC = LevelController.instance;
    }
    public void AddEnemy(int type)
    {
        EnemyType t = (EnemyType)type;
       foreach (var item in Enemies.ToArray())
        {
            if(item.type==t)
            {
                currentEnemies.Add(item.Enemy);
            }
        }
    }
    public void RemoveEnemy(int type)
    {
        EnemyType t = (EnemyType)type;
        foreach (var item in Enemies.ToArray())
        {
            if (item.type == t)
            {
                currentEnemies.Remove(item.Enemy);
            }
        }
    }
    
}

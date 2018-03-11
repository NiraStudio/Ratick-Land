using System.Collections;
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
    [Header("PowerRate Parameters")]
    public float FormulaBase;
    public float FormulaChanger;

    int powerRate;
    LevelController LC;

    void Start()
    {
        LC = LevelController.instance;
        foreach (var item in events)
        {
            item.Invoke();
        }
        powerRate = PowerRate();
        Spawn();
    }
    public void AddEnemy(int type)
    {
        EnemyType t = (EnemyType)type;
       foreach (var item in Enemies.ToArray())
        {
            if(item.type==t)
            {
                currentEnemies.Add(item.Enemy);
                print("aa");
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
    public void Spawn()
    {
        int a = 0;
        do
        {
            Vector3 t = Random.insideUnitCircle * 0.2f;
            GameObject g = Instantiate(currentEnemies[Random.Range(0, currentEnemies.Count)], transform.position + t, Quaternion.identity);
            g.transform.SetParent(transform);
            a += g.GetComponent<Enemy>().data.PowerRate;
        } while (a<powerRate);
           
    }
    public int PowerRate()
    {
        int answer = 0;
        answer =(int)( Mathf.Pow(FormulaChanger, LC.BrokenCage) * (FormulaBase - LC.BrokenCage));

        return answer;
    }
    
    
}

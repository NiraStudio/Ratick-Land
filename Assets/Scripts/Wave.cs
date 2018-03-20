using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Pool;

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
    public bool Gurdian;
    public List<GameObject> currentEnemies = new List<GameObject>();
    public List<WaveEnemy> Enemies = new List<WaveEnemy>();
    public List<GameObject> SpawnedEnemies = new List<GameObject>();
    [Header("MeleeNormal = 0,MeleeFast = 1,RangeShort = 2,RangeLong = 3,Tank = 4,EliteMeleeNormal = 5 ")]
    [Header("EliteMeleeFast = 6,EliteRangeShort = 7, EliteRangeLong = 8, EliteTank = 9")]
    public UnityEvent[] events=new UnityEvent[10];
    [Header("PowerRate Parameters")]
    public float FormulaBase;
    public float FormulaChanger;

    int powerRate=70,enemyNumber;
    LevelController LC;
    void Start()
    {
        LC = LevelController.instance;
        for (int i = 0; i <= LC.BrokenCage; i++)
        {

            events[i].Invoke();
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
            GameObject g =LeanPool.Spawn(  currentEnemies[Random.Range(0, currentEnemies.Count)], transform.position + t, Quaternion.identity);
            g.GetComponent<Enemy>().Gurdian = Gurdian;
            g.GetComponent<Enemy>().Father = this;
            g.transform.SetParent(transform);
            a += g.GetComponent<Enemy>().data.PowerRate;
            SpawnedEnemies.Add(g);
            enemyNumber++;
        } while (a<powerRate);
           
    }
    public int PowerRate()
    {
        int answer = 0;
        answer =(int)( Mathf.Pow(FormulaChanger, LC.BrokenCage) * (FormulaBase - LC.BrokenCage));

        return answer;
    }
    public void Decrease()
    {
        enemyNumber -= 1;
        if (enemyNumber <= 0)
            Destroy(gameObject);
    }
    
    
}

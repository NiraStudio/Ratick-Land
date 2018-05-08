using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Pool;

public class Wave : MonoBehaviour{

    public GameObject[] Enemies;
    public float range,Distance;
    public IntRange EnemyAmount;
    [HideInInspector]
    public bool moving;
    public int remaining;
    public bool Open;
    int breakPoint = 40;
    int baseMin, baseMax;

    void Awake()
    {
        baseMax = EnemyAmount.m_Max;
        baseMin = EnemyAmount.m_Min;
    }
    void Start()
    {
       
    }
    public void Spawn()
    {
        StartCoroutine(StartSpawn());
    }
   
    public IEnumerator StartSpawn()
    {
        ConfigureRandomness();
        int a = EnemyAmount.Random;
        Open = true;
        for (int i = 0; i < a; i++)
        {
            Vector2 t = transform.position;
            t += Random.insideUnitCircle * range;
            Instantiate(Enemies[Random.Range(0,Enemies.Length)], t, Quaternion.identity);
        }
        yield return new WaitForSeconds(7);
        Open = false;

    }

    public void ConfigureRandomness()
    {
        remaining = (int)GamePlayManager.instance.MatchTime - (int)GamePlayManager.instance.remainingTime;
        float Power = 50;

        if (remaining > 60)
            remaining = 60;
        

        float min = baseMin + ((remaining / Power) * baseMin);
        float max = baseMax + ((remaining / Power) * baseMax);



        EnemyAmount.m_Min = (int)Mathf.Round(min);
        EnemyAmount.m_Max = (int)Mathf.Round(max);

    }

    void OnDrawGizmos()
    {
        Vector2 tt = Vector2.zero - (Vector2)transform.position;
        tt =(Vector2) transform.position + (tt * Distance);
        Gizmos.DrawLine(transform.position, tt);
    }


   




}
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /* {

    public enum EnemyType
    {
        MeleeNormal,MeleeFast,RangeShort,RangeLong,Tank,EliteMeleeNormal,EliteMeleeFast,EliteRangeShort, EliteRangeLong, EliteTank
    }

    public List<WaveEnemy> Enemies = new List<WaveEnemy>();
    List<GameObject> currentEnemies = new List<GameObject>();
    List<GameObject> SpawnedEnemies = new List<GameObject>();
    [Header("MeleeNormal = 0,MeleeFast = 1,RangeShort = 2,RangeLong = 3,Tank = 4,EliteMeleeNormal = 5 ")]
    [Header("EliteMeleeFast = 6,EliteRangeShort = 7, EliteRangeLong = 8, EliteTank = 9")]
    public UnityEvent FirstArrange;
    public UnityEvent[] events=new UnityEvent[10];


    [Header("PowerRate Parameters")]
    public float FormulaBase;
    public float FormulaChanger;

    public int powerRate=70,enemyNumber;

    [HideInInspector]
    public bool created;
    GamePlayManager LC;
    [HideInInspector]
    public WavePointHolder wph;
    void Start()
    {
        LC = GamePlayManager.instance;
        FirstArrange.Invoke();
        int a = LC.BrokenCage < events.Length ? LC.BrokenCage : 10;
        for (int i = 0; i < a; i++)
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
    public void Update()
    {
        if (transform.childCount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (wph)
            wph.StartCountDown();
        if (!Guradian)
            WaveController.Instance.DecreaseWave(gameObject);
        Destroy(gameObject);
    }

    [System.Serializable]
    public class WaveEnemy
    {
        public EnemyType type;
        public GameObject Enemy;

    }

}*/

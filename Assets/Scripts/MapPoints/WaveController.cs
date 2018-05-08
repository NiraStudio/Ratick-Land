using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    #region Singleton

    public static WaveController Instance;
    void Awake()
    {
        Instance = this;
    }

    #endregion


    public List<WavePoint> waves = new List<WavePoint>();
    public float MinWaveRange, MaxWaveRange, checkWaitTime;
    public float MaxWaves;

    public List<GameObject> currentWaves=new List<GameObject>();
    Vector2 cameraPos;
    GameObject map;
	// Use this for initialization
	void Start () {


        map = GameObject.FindWithTag("Map");
        //get waves
        for (int i = 0; i < map.transform.childCount; i++)
        {
            if (map.transform.GetChild(i).GetComponent<MapClass>())

                waves.AddRange(map.transform.GetChild(i).GetComponent<MapClass>().WavePoints);
        }


        StartCoroutine(CheckForSpawn());
    }

    // Update is called once per frame
    void Update () {
        cameraPos = Camera.main.transform.position;

        foreach (var wave in currentWaves.ToArray())
        {
            if (Vector2.Distance(wave.transform.position, cameraPos) > MaxWaveRange)
            {
            }
        }
	}

    IEnumerator CheckForSpawn()
    {
        int times=0;
        while (currentWaves.Count<MaxWaves&&times< MaxWaves)
        {
            foreach (var wave in waves.ToArray())
            {
                float dis = Vector2.Distance(wave.transform.position, cameraPos);
                if (dis >= MinWaveRange&&dis<MaxWaveRange)
                {
                    
                }
            }
            times++;
        }

        yield return new WaitForSeconds(checkWaitTime);
        StartCoroutine(CheckForSpawn());
    }
    public void DecreaseWave(GameObject ob)
    {
        currentWaves.Remove(ob);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.transform.position, MinWaveRange);
        Gizmos.DrawWireSphere(Camera.main.transform.position, MaxWaveRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public GameObject[] enemies;
    public IntRange enemiesNumber;
    public LevelController controller;
    [Tooltip("Increase enemies number by this after every 10 Sec")]
    public int increaseNumber;
    int lenght;
	// Use this for initialization
    void Start()
    {
        int t =(int) controller.LevelTime;
        enemiesNumber.m_Max += (int)((t / 10) * increaseNumber);
        enemiesNumber.m_Min += (int)((t / 10) * increaseNumber);
        for (int i = 0; i < enemiesNumber.Random; i++)
        {
            Vector2 p = Random.insideUnitCircle * 1;
            p =(Vector2) transform.position + p;
            GameObject g = Instantiate(enemies[Random.Range(0,enemies.Length)], p, Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
        }
        StartCoroutine(checkForEnd());
        
    }
	
	// Update is called once per frame
    IEnumerator checkForEnd()
    {
        
        if (transform.childCount == 0)
        {
            controller.currentWaves.Remove(gameObject);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(checkForEnd());
    }
    IEnumerator SelfKiller()
    {
        yield return new WaitForSeconds(20);
        if (Vector2.Distance(transform.position, GameObject.FindWithTag("Aim").transform.position) > 7)
        {
            controller.currentWaves.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}

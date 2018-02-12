using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public bool GurdianWave;
    public GameObject[] enemies;
    public IntRange enemiesNumber;
    public LevelController controller;
    [Tooltip("Increase enemies number by this after every 10 Sec")]
    public int increaseNumber;
    int lenght;
	// Use this for initialization
    void Start()
    {
        controller = LevelController.instance;
        
        for (int i = 0; i < enemiesNumber.Random; i++)
        {
            Vector2 p = Random.insideUnitCircle * 0.5f;
            p =(Vector2) transform.position + p;
            GameObject g = Instantiate(enemies[Random.Range(0,enemies.Length)], p, Quaternion.identity);
            g.GetComponent<Enemy>().Gurdian=GurdianWave;
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
    
}

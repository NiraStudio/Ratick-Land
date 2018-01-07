using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public GameObject enemy;
    public IntRange enemiesNumber;
    List<GameObject> myEnemies=new List<GameObject>();
    public LevelController controller;
    int lenght;
	// Use this for initialization
    void Start()
    {
        for (int i = 0; i < enemiesNumber.Random; i++)
        {
            Vector2 p = Random.insideUnitCircle * 2;
            p =(Vector2) transform.position + p;
            GameObject g = Instantiate(enemy, p, Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
            myEnemies.Add(g);
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

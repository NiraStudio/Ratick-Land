﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Cage : MonoBehaviour, IHitable
{
    public float hitPoint;
    public CharacterDataBase data;
    List<GameObject> prisoners=new List<GameObject>();
    public GameObject Wave;
    public Transform RightWavePos, LeftWavePos;
    LevelController LC;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        addCharacters();
        Instantiate(Wave, RightWavePos.position, Quaternion.identity);
        Instantiate(Wave, LeftWavePos.position, Quaternion.identity);
        sr = GetComponent<SpriteRenderer>();
        LC = LevelController.instance;
        Destroy(RightWavePos.gameObject);
        Destroy(LeftWavePos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      //   sr.sortingOrder= IsoMetricHandler.giveSortingOrderNumber(transform.position.y);
      //  IsoMetricHandler.ChangeByTransform(transform);

    }
    void addCharacters()
    {
        int r = Random.Range(3, 6);
        for (int i = 0; i < r; i++)
        {
            Vector2 a = Random.insideUnitCircle * 0.3f;
            a.y = Random.Range(0.01f, 0.1f);
            a = (Vector2)transform.position + a;
            GameObject g = Instantiate(data.GiveByID(1).prefab, a, Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
            g.GetComponent<Character>().Release(false);
            prisoners.Add(g);
        }
    }

    public void GetHit(float dmg)
    {
        if (LC.keyCount <= 0)
            return;

        hitPoint -= dmg;
        if(hitPoint<0)
            Die();
    }
    

    public void Die()
    {
        foreach (var item in prisoners)
        {
            item.GetComponent<Character>().Release(true);
            item.transform.SetParent(null);
            LevelController.instance.AddCharacters(item);
        }
        LevelController.instance.MakeCage();
        LC.ChangeKeyCount(-1);
        Destroy(gameObject);
    }
}

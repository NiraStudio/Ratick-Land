using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Cage : MonoBehaviour, IHitable
{
    public float hitPoint;
    public CharacterDataBase data;
    List<GameObject> prisoners=new List<GameObject>();

    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        addCharacters();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
         sr.sortingOrder= IsoMetricHandler.giveSortingOrderNumber(transform.position.y);
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
        Destroy(gameObject);
    }
}

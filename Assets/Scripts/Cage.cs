using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Cage : MonoBehaviour, IHitable
{
    public GameObject Poof;
    public float hitPoint;
    public CharacterDataBase data;
    public GameObject Wave;
    public Transform[] Poses;
    List<GameObject> prisoners=new List<GameObject>();

    GameObject p;
    KeyManager KM;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        int a = Random.Range(0, 4);
        for (int i = 0; i < Poses.Length; i++)
        {
            if (i != a)
            {
                Instantiate(Wave, Poses[i].transform.position,Quaternion.identity);
                Destroy( Poses[i].gameObject);
            }
        }
        sr = GetComponent<SpriteRenderer>();
        KM = KeyManager.Instance;
    }

    // Update is called once per frame
    
    void addCharacters()
    {
        int r = Random.Range(3, 6);
        for (int i = 0; i < r; i++)
        {
            Vector2 a = Vector2.zero ;
            a.y = Random.Range(0.01f, 0.1f);
            a = (Vector2)transform.position + a;
            GameObject g = Instantiate(data.GiveByID(2).prefab, a, Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
            g.GetComponent<Character>().Release(true);
            prisoners.Add(g);
        }
    }

    public void GetHit(float dmg)
    {
        if (KM.keyCount <= 0)
            return;

        hitPoint -= dmg;
        if(hitPoint<0)
            Die();
    }
    

    public void Die()
    {
        addCharacters();
        foreach (var item in prisoners)
        {
            item.GetComponent<Character>().Release(true);
            item.transform.SetParent(null);
            LevelController.instance.AddCharacters(item);
        }
        LevelController.instance.MakeCage();
        KM.ChangeKeyCount(-1);
        Instantiate(Poof, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

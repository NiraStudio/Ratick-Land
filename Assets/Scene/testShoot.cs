using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testShoot : MonoBehaviour {
   // public EZObjectPools.EZObjectPool e;
    public bool aaa;
    public GameObject target,Bullet;
    public Chest s;
    public Skin[] skins;
    public string[] skissssns;
    GameObject GO;


    // Use this for initialization
    void Start () {

        //skins = target.GetComponents<Skin>();
        skissssns = new string[skins.Length];
        for (int i = 0; i < skins.Length; i++)
        {
            skissssns[i] = skins[i].skinName;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // e.TryGetNextObject(Vector3.zero, Quaternion.identity);
            

        }
       // GetComponent<Canvas>().worldCamera = Camera.main;

    }
    void spawnBullet()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<AimedProjectile>().Spawn(target, 10);
    }
    void openChest()
    {
        s.MakeChest();
    }
    public string GiveID()
    {
        string answer = "";
        bool find = true;
        for (int i = 0; i < 10; i++)
        {
            int a = Random.Range(1, 3);
            if(a==1)
            {
                answer += (char)Random.Range(48, 58);
            }
            else
            {
                answer += (char)Random.Range(65, 90);
            }
        }
        
        print(answer);
        return answer;
    }
    public void Tests()
    {
        InformationPanel.Instance.OpenADRewardPanel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Enemy : MonoBehaviour,IHitable {
    public EnemyData data;
    
    protected ObscuredFloat speedMultiPly;
    protected ObscuredFloat attackSpeed;
    protected ObscuredFloat hitPoint;
    protected ObscuredFloat damage;
    protected ObscuredFloat speed;
    protected ObscuredInt coin;

    [SerializeField]
    GameObject coinObject;
    LevelController levelController;
	// Use this for initialization
	public virtual void Start () {
        levelController = LevelController.instance;
        RenewData();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHit(float dmg)
    {
        hitPoint -= dmg;
        if (hitPoint < 0)
            Death();
    }
    void Death()
    {
        ///coin Ui
        Instantiate(coinObject, transform.position, Quaternion.identity).transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = coin.ToString();
        Destroy(gameObject);
    }
    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = data.damage;
        damage = damage * ((Time.time / 20)+1);
        coin = data.coin * levelController.WorldCoinMultiply;
    }
}

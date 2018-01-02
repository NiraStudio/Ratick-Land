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

	// Use this for initialization
	public virtual void Start () {
        RenewData();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHit(float dmg)
    {
        hitPoint -= dmg;
        print(hitPoint+" damge"+dmg);
    }
    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = data.damage;
    }
}

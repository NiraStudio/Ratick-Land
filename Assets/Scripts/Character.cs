using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MainBehavior
{
    public CharacterData data;
    public float speed;
    GameObject aimer;
    LevelController controller;
    Rigidbody2D rg;
    Animator anim;

    //vars
    float speedMultiPly;
    float attackSpeed;
    float hitPoint;
    float damage;

    Vector2 t;
	// Use this for initialization
	void Start () {
        aimer = LevelController.instance.aimer;
        controller = LevelController.instance;
        rg = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        RenewData();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(controller.Move)
        {
            //transform.position = Vector2.MoveTowards(transform.position, aimer.transform.position, speed * Time.deltaTime);
            speed = (aimer.transform.position - transform.position).magnitude*speedMultiPly;
            t =(aimer.transform.position - transform.position).normalized;
            t = (Vector2)transform.position + (t*(speed*Time.deltaTime)) ;
            rg.MovePosition(t);
    
        }

        anim.SetBool("Move", controller.Move);
	}
    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = data.damage;

    }
}

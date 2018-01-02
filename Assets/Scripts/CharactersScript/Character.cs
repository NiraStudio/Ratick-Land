using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Character : MainBehavior
{
    public CharacterData data;
    protected GameObject aimer;
    protected LevelController controller;
    protected Rigidbody2D rg;
    protected Animator anim;
    protected SpriteRenderer sr;
    protected LayerMask EnemyMask;

    //vars
    protected ObscuredFloat speedMultiPly;
    protected ObscuredFloat attackSpeed;
    protected ObscuredFloat hitPoint;
    protected ObscuredFloat damage;
    protected ObscuredFloat speed;


    Vector2 t, tt;
    protected bool free;
    
    // Use this for initialization
    void Start()
    {
        aimer = LevelController.instance.aimer;
        controller = LevelController.instance;
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        EnemyMask = 1 << 8;
        sr.sortingOrder = IsoMetricHandler.giveSortingOrderNumber(transform.position.y);
        RenewData();
    }
    public virtual void Update()
    {
        if (!free)
            return;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!free)
            return;
        #region Attack
        if (controller.Move)
        {
            speed = (aimer.transform.position - transform.position).magnitude * speedMultiPly;
            tt = (aimer.transform.position - transform.position).normalized;
            tt = tt * speed;
            // t = (Vector2)rg.position + tt ;
            // t = (Vector2)transform.position + (Vector2.one * 1 * Time.fixedDeltaTime);
            // rg.MovePosition(t);
            rg.velocity = tt;
        }
        else
            rg.velocity = Vector3.zero;

        #endregion

        ///ISOMETRIC
        sr.sortingOrder = IsoMetricHandler.giveSortingOrderNumber(transform.position.y);



       // if (anim)
         //   anim.SetBool("Move", controller.Move);
    }
    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = data.damage;
    }
    public void Release(bool state)
    {

        free = state;
        GetComponent<Collider2D>().enabled = state;
    }
    
}

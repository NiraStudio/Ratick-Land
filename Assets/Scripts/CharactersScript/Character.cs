using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Character : MainBehavior,IAttackable,IHitable
{
    public CharacterData data;
    public bool right;
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
    protected IntRange damage;
    protected ObscuredFloat speed;
    protected ObscuredFloat attackRange;
    protected Collider2D detectedEnemy;

    public int HP
    {
        get { return (int) hitPoint; }
    }

    float waitTime;
    Vector2 t, tt;
    protected bool free;


    public void Awake()
    {
        RenewData();

    }
    public virtual void Start()
    {
        controller = LevelController.instance;
        aimer = controller.aimer;
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        EnemyMask = 1 << 8;
        sr.sortingOrder = IsoMetricHandler.giveSortingOrderNumber(transform.position.y);
    }
   
    public virtual void FixedUpdate()
    {
        
        if (!free)
            return;

        #region Move
        if (controller.Move)
        {
            speed = speedMultiPly;
           
            tt = (aimer.transform.position - transform.position);
            tt.Normalize();
            tt = tt * speed;
            if (Vector2.Distance(transform.position, aimer.transform.position) > 0.2f)
                rg.velocity = tt;
            else
                rg.velocity = Vector2.zero;
            
            if (aimer.transform.position.x > transform.position.x && !right)
            {
                Flip();
            }
            else if (aimer.transform.position.x < transform.position.x && right)
            {
                Flip();

            }
        }
        else
            rg.velocity = Vector3.zero;

        #endregion

        #region Attack
        detectedEnemy = Physics2D.OverlapCircle(transform.position, attackRange, EnemyMask);
        if (detectedEnemy)
        {
            waitTime += Time.deltaTime;

            if (waitTime > attackSpeed && detectedEnemy)
                Attack();
        }
        else
            waitTime = 0;
       


        ///ISOMETRIC
        sr.sortingOrder = IsoMetricHandler.giveSortingOrderNumber(transform.position.y);

        #endregion

        if (anim)
            anim.SetBool("Move", controller.Move);
    }




    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = data.damage;
        attackRange = data.attackRange;
    }

    public void Release(bool state)
    {

        free = state;
        GetComponent<Collider2D>().enabled = state;
    }

    void Flip()
    {

        sr.flipX = !sr.flipX;
        right = !right;
    }

    public virtual void Attack()
    {
        waitTime = 0;
    }

    public virtual void GetHit(float dmg)
    {

        hitPoint -= dmg;
        if (hitPoint <= 0)
            Die();
    }

    public virtual void Die()
    {
        //animation
        controller.RemoveCharacter(gameObject);
        Destroy(gameObject);
    }

    public void UpgradeTheCharacter(int Level)
    {
        for (int i = 0; i < Level; i++)
        {
            switch (data.upgrade.type)
            {
                case Upgrade.Type.MinDamage:
                    damage.m_Min += data.upgrade.amount;
                    break;

                case Upgrade.Type.MaxDamage:
                    damage.m_Max += data.upgrade.amount;
                    break;

                case Upgrade.Type.Damage:
                    damage.m_Min += data.upgrade.amount;
                    damage.m_Max += data.upgrade.amount;
                    break;

                case Upgrade.Type.Hp:
                    hitPoint += data.upgrade.amount;
                    break;
            }
        }
    }
}

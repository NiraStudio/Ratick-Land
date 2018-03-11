using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using System;

[RequireComponent(typeof(SkinManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MainBehavior,IAttackable,IHitable,IHealable
{
    public CharacterData data;
    public SkinDB skinDB;
    public GameObject CenterPoint;
    public bool right;
    protected SkinManager skinManager;
    protected GameObject aimer;
    protected LevelController controller;
    protected GamePlayInput GPI;
    protected Rigidbody2D rg;
    protected Animator anim;
    protected SpriteRenderer sr;

    //vars
    protected ObscuredFloat speedMultiPly;
    protected ObscuredFloat attackSpeed;
    protected ObscuredFloat hitPoint;
    protected IntRange damage;
    protected ObscuredFloat speed;
    protected ObscuredFloat attackRange;
    protected Collider2D detectedEnemy;
    protected bool Attacking;

    public int HP
    {
        get { return (int) hitPoint; }
    }

    float waitTime;
    Vector2 t, tt;
    protected bool free;
    float MaxHp;

    public void Awake()
    {
        controller = LevelController.instance;
        GPI = GamePlayInput.Instance;
        if (controller == null)
        {
            this.enabled = false;
            return;
        }
        RenewData();
        UpgradeTheCharacter(GameManager.instance.CharacterLevel(data.id));
        
    }
    public virtual void Start()
    {
        

        aimer = GPI.aimer;
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        skinManager = GetComponent<SkinManager>();
       // sr.sortingOrder = IsoMetricHandler.giveSortingOrderNumber(transform.position.y);
        IsoMetricHandler.ChangeByTransform(transform);
        anim.GetBehaviour<AttackingStateMachine>().OnExit.AddListener(AttackAllower);

    }
   
    public virtual void FixedUpdate()
    {
        
        if (!free)
            return;

        #region Move
        if (GPI.Move)
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
        detectedEnemy = Physics2D.OverlapCircle(CenterPoint.transform.position, attackRange, MainBehavior.EnemyLayer);
        if (detectedEnemy)
        {
            waitTime += Time.deltaTime;

            if (waitTime > attackSpeed && detectedEnemy&&!Attacking)
                AttackAnimation();
        }
        else
            waitTime = 0;
       


        ///ISOMETRIC
       // sr.sortingOrder = IsoMetricHandler.giveSortingOrderNumber(transform.position.y);
        IsoMetricHandler.ChangeByTransform(transform);


        #endregion

        if (anim)
            anim.SetBool("Move", GPI.Move);
    }




    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = new IntRange(data.damage.m_Min * controller.WorldAttackMultiPly, data.damage.m_Max * controller.WorldAttackMultiPly);
        attackRange = data.attackRange;
        MaxHp = hitPoint;
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
        detectedEnemy.SendMessage("GetHit", (float)damage.Random);
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
            foreach (var item in data.UpgradesForEachLevel)
            {
                switch (item.type)
                {
                    case Upgrade.Type.MinDamage:
                        damage.m_Min += item.amount;
                        break;

                    case Upgrade.Type.MaxDamage:
                        damage.m_Max += item.amount;
                        break;

                    case Upgrade.Type.Damage:
                        damage.m_Min += item.amount;
                        damage.m_Max += item.amount;
                        break;

                    case Upgrade.Type.Hp:
                        hitPoint += item.amount;
                        break;
                }
            }
            
        }
    }

    public void AttackAllower()
    {
        Attacking = false;
        waitTime = 0;
    }

    public virtual void AttackAnimation()
    {
        Attacking = true;
        Attack();
        AttackAllower();
    }








    void Reset()
    {
        GameObject a = new GameObject();
        a.transform.SetParent(transform);
        a.name = "Skins";

        skinManager = GetComponent<SkinManager>();


        gameObject.layer = 10;
        skinManager.skinHolder = a;

        if (gameObject.GetComponent<CircleCollider2D>() == null)
            gameObject.AddComponent<CircleCollider2D>();

        /*Object s = Resources.Load("FirctionLess");
        rg.sharedMaterial =(PhysicsMaterial2D) s as PhysicsMaterial2D;*/

    }

    public void GetHeal(float amount)
    {
        hitPoint +=(int) amount;
        if (hitPoint > MaxHp)
            hitPoint = MaxHp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

[RequireComponent(typeof(SkinManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MainBehavior,IAttackable,IHitable
{
    public CharacterData data;
    public SkinDB skinDB;
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


    public int HP
    {
        get { return (int) hitPoint; }
    }

    float waitTime;
    Vector2 t, tt;
    protected bool free;
    

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
        detectedEnemy = Physics2D.OverlapCircle(transform.position, attackRange, MainBehavior.EnemyLayer);
        if (detectedEnemy)
        {
            waitTime += Time.deltaTime;

            if (waitTime > attackSpeed && detectedEnemy)
                Attack();
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
}

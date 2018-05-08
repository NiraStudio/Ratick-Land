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
    public GameObject CenterPoint;
    public bool right;
    protected SkinManager skinManager;
    protected GameObject Aimer;
    protected GamePlayManager GPM;
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
    public bool Attacking,Gathering;
    protected bool isLeader=false;
    protected GameObject DmgPopUp;

    public int HP
    {
        get { return (int) hitPoint; }
    }

    protected float waitTime;
    Vector2 t, tt;
    protected bool free;
    float MaxHp;
    CharacterMoveState situation;
    KeyManager keyManager;
    XpController XPC;
    public void Awake()
    {
        GPM = GamePlayManager.instance;
        GPI = GamePlayInput.Instance;
        XPC = XpController.Instance;
        if (GPM == null)
        {
            this.enabled = false;
            GetComponent<IsoMetricHandler>().enabled = false;
            return;
        }
        DmgPopUp = Resources.Load("DmgPopUp", typeof(GameObject)) as GameObject;

        RenewData();
        UpgradeTheCharacter(GameManager.instance.CharacterLevel(data.id));
        
    }
    public virtual void Start()
    {

        Aimer = GameObject.FindWithTag("Aim");
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        skinManager = GetComponent<SkinManager>();
        speed = speedMultiPly;
    }

    public virtual void Update()
    {

        if (!free)
            return;

        if (GPM.gameState != GamePlayState.Playing)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        #region Move

        if (!Gathering)
        {

            if (GPI.Move && !Attacking)
            {

                rg.bodyType = RigidbodyType2D.Dynamic;

                tt = GPI.Direction;
                tt = tt * speed * GPM.WorldSpeedMultiPly;
                rg.velocity = tt;

                if (tt.x > 0 && !right)
                {
                    Flip();
                }
                else if (tt.x < 0 && right)
                {
                    Flip();

                }



            }
            else
            {
                if (detectedEnemy != null)
                {
                    tt = (detectedEnemy.transform.position - transform.position);
                    if (tt.x > 0 && !right)
                    {
                        Flip();
                    }
                    else if (tt.x < 0 && right)
                    {
                        Flip();

                    }
                }
                if (rg.bodyType != RigidbodyType2D.Static)
                    rg.velocity = Vector2.zero;
                rg.bodyType = RigidbodyType2D.Static;
            }
        }
        #endregion

        #region Attack
            detectedEnemy = Physics2D.OverlapCircle(CenterPoint.transform.position, attackRange, CageLayer);
        if (detectedEnemy == null)
            detectedEnemy = Physics2D.OverlapCircle(CenterPoint.transform.position, attackRange, EnemyLayer);

        if (waitTime > attackSpeed / GPM.WorldSpeedMultiPly && detectedEnemy != null && !Attacking)
            AttackAnimation();

        waitTime += Time.deltaTime;
        #endregion

        if (anim)
        {
            anim.SetBool("Moving", GPI.Move);
            anim.speed = GPM.WorldSpeedMultiPly;
        }
    }

    public void StartGathering()
    {
        if (gameObject.tag == "Leader")
            return;
        Gathering = true;
        StartCoroutine(Gather());
    }
    public IEnumerator Gather()
    {

        Vector2 direction;
        Collider2D detectedAlly;
        Transform leader = GameObject.FindWithTag("Leader").transform;

        while (Vector2.Distance(transform.position, leader.position) > 0.2f)
        {
            detectedAlly= detectedEnemy = Physics2D.OverlapCircle(CenterPoint.transform.position, attackRange, CharacterLayer);
            if (detectedAlly && Vector2.Distance(transform.position, leader.position) < GPM.CharacterAmount*0.04f)
                break;
            rg.bodyType = RigidbodyType2D.Dynamic;
            direction = leader.position - transform.position;
            direction.Normalize();
            rg.velocity = direction * speed * GPM.WorldSpeedMultiPly;
            yield return null;
        }
        Gathering = false;
    }
   
    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = new IntRange(0,0);
        damage.m_Max =(int)( data.damage * GPM.WorldAttackMultiPly);
        damage.m_Min=(int)(( data.damage-(data.damage*0.2f)) * GPM.WorldAttackMultiPly);
        attackRange = data.attackRange;
    }

    public void Release(bool state)
    {

        free = state;
        GetComponent<Collider2D>().enabled = state;
    }

    void Flip()
    {

        Vector3 aa = transform.localScale;
        aa.x *= -1;
        transform.localScale = aa;
        right = !right;
    }

    public virtual void Attack()
    {
        if (detectedEnemy != null)
        {
            float f = damage.Random;
            f = f + (f * XPC.AttackIncrease);
            try
            {
                detectedEnemy.SendMessage("GetHit", f);
            }
            catch (Exception)
            {

                throw;
            }
            Instantiate(DmgPopUp, detectedEnemy.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(((int)f).ToString(), DmgPopUpBehaivior.AttackType.playerAttack, detectedEnemy.gameObject.transform.position);
        }
        waitTime = 0;
        Attacking = false;


    }

    public virtual void GetHit(float dmg)
    {

        hitPoint -= dmg;
        if (hitPoint <= 0)
            Die();
    }
    public void RecoverHp()
    {
        hitPoint = MaxHp;
    }

    public virtual void Die()
    {
        //animation
        GPM.RemoveCharacter(gameObject);
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
        MaxHp = hitPoint;

    }

    public void AttackAllower()
    {
        Attacking = false;
        waitTime = 0;
    }

    public virtual void AttackAnimation()
    {
        Attacking = true;
        anim.SetTrigger("Attack");
    }








    public virtual void Reset()
    {
        GameObject a = new GameObject();
        a.transform.SetParent(transform);
        a.name = "Skins";

        skinManager = GetComponent<SkinManager>();


        gameObject.layer = 10;
        skinManager.skinHolder = a;

        if (gameObject.GetComponent<CircleCollider2D>() == null)
            gameObject.AddComponent<CircleCollider2D>();
        GetComponent<IsoMetricHandler>().Center = gameObject;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
public enum CharacterMoveState
{
    InArea,Far
}

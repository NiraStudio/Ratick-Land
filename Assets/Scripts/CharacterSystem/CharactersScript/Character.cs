﻿using System.Collections;
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
    protected LevelController LC;
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
    public void Awake()
    {
        LC = LevelController.instance;
        GPI = GamePlayInput.Instance;
        keyManager = KeyManager.Instance;
        if (LC == null)
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

        if (LC.gameState != GamePlayState.Playing)
            return;

        #region Move


        if (GPI.Move && !Attacking)
        {
            rg.bodyType = RigidbodyType2D.Dynamic;

            tt = (Aimer.transform.position - transform.position);
            tt.Normalize();
            tt = tt * speed;
            rg.velocity = tt;
            if (Vector2.Distance(transform.position, Aimer.transform.position) > 0.1f)
            {
                if (tt.x > 0 && !right)
                {
                    Flip();
                }
                else if (tt.x < 0 && right)
                {
                    Flip();

                }
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
            rg.velocity = Vector2.zero;
            rg.bodyType = RigidbodyType2D.Static;
        }

        #endregion

        #region Attack
        if (keyManager.keyCount > 0)
            detectedEnemy = Physics2D.OverlapCircle(CenterPoint.transform.position, attackRange, CageLayer);
        else
            detectedEnemy = null;
        if (detectedEnemy == null)
            detectedEnemy = Physics2D.OverlapCircle(CenterPoint.transform.position, attackRange, EnemyLayer);

        if (waitTime > attackSpeed && detectedEnemy != null && !Attacking)
            AttackAnimation();

        waitTime += Time.deltaTime;


        #endregion

        if (anim)
            anim.SetBool("Moving", GPI.Move);
    }


   
    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        hitPoint = data.hitPoint;
        damage = new IntRange(0,0);
        damage.m_Max = data.damage * LC.WorldAttackMultiPly;
        damage.m_Min=(int)( data.damage-(data.damage*0.2f)) * LC.WorldAttackMultiPly;
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
            int f = damage.Random;
            try
            {
                detectedEnemy.SendMessage("GetHit", (float)f);
            }
            catch (Exception)
            {

                throw;
            }
            Instantiate(DmgPopUp, detectedEnemy.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(f.ToString(), DmgPopUpBehaivior.AttackType.playerAttack, detectedEnemy.gameObject.transform.position);
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

    public virtual void Die()
    {
        //animation
        LC.RemoveCharacter(gameObject);
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

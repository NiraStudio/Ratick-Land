using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CodeStage.AntiCheat.ObscuredTypes;
using System;

public class Boss : MainBehavior,IHitable,IAttackable {
    public BossAction[] Actions;
    public BossData data;
    public IntRange attackCoolDown=new IntRange(0,0);
    public GameObject LineHitter,target;

    protected ObscuredString bossName;
    protected ObscuredFloat damage;
    protected ObscuredInt hitPoint;
    protected ObscuredFloat range;

    LevelController LC;
    Collider2D[] temp;
    LayerMask charactersLayer;
    Animator anim;
    Vector2 ChoosedDirection;

    [SerializeField]
    bool right;
    bool Counter;
    float angle,time;
    int waitTime;
	// Use this for initialization
	void Start () {
        //Sorting The Actions In Order
        Sorting();
        RenewData();
        anim = GetComponent<Animator>();
        charactersLayer = 1 << 10;
        Vector2 t= LineHitter.transform.localScale;
        t.y = range ;
        LineHitter.transform.localScale = t;
        waitTime = attackCoolDown.Random;
        LC = LevelController.instance;
    }
	

    void RenewData()
    {
        this.bossName = data.bossName;
        this.damage = data.damage;
        this.hitPoint = data.hitPoint;
        this.range = data.range;
    }

    // Update is called once per frame
    void Update()
    {
        if (LC.gameState==GamePlayState.Finish)
            return;
        if (!Counter)
            return;


        temp = Physics2D.OverlapCircleAll(transform.position, range * 1.5f, charactersLayer);
        if (temp.Length > 0)
        {
            time += Time.deltaTime;
            if (time >= waitTime)
                Attack();
        }
        else
        {
            time = 0;
        }
    }

    void Sorting()
    {
        BossAction aa = new BossAction();
        for (int i = 0; i < Actions.Length - 1; i++)
            for (int j = 0; j < Actions.Length - 1; j++)
                if (Actions[j].chance > Actions[j + 1].chance)
                {
                    aa = Actions[j];
                    Actions[j] = Actions[j + 1];
                    Actions[j + 1] = aa;
                }
    }
    public void ChooseAction()
    {
        float dice;
        float temp;

        dice =UnityEngine. Random.Range(0, 101);
        temp = 0;
        for (int i = 0; i < Actions.Length; i++)
        {
            temp += Actions[i].chance;
            if (dice < temp)
            {
                anim.SetTrigger(Actions[i].ActionName);
                break;
            }
        }

    }

    public void Splash()
    {
        Collider2D[] temp = Physics2D.OverlapCircleAll(transform.position, range,charactersLayer);
        foreach (var item in temp)
        {
            item.SendMessage("GetHit", damage);
        }
        Counter = true;

    }
    public void ChooseRandomDirection()
    {
        Vector2 dir = target.transform.position - LineHitter.transform.position;
         angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        LineHitter.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        ChoosedDirection = dir;

        ChoosedDirection = new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);
        print(ChoosedDirection);

        
        
    }
    public void AttackDirection()
    {
        temp = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.5f, range * 2),angle, charactersLayer);
        foreach (var item in temp)
        {
            Vector2 dir = item.transform.position - transform.position;
            Vector2 t= new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);
            if(t==ChoosedDirection)
            print(item.gameObject.name);
        }
        Counter = true;
    }
    public void Flip()
    {
        Vector3 t = transform.localScale;
        t.x *=-1;
        right = !right;
        transform.localScale = t;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawCube(transform.position, Quaternion.Euler(0, 0, angle), new Vector2(0.5f, range * 2));
    }

    

    public void GetHit(float dmg)
    {
        hitPoint -=(int) dmg;
        if (hitPoint <= 0)
        {
            hitPoint = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        ChooseAction();
        Counter = false;
    }
}


[System.Serializable]
public class BossAction
{
    public string ActionName;
    
    public int chance;
}

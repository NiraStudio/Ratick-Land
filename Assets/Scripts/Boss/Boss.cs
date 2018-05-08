using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CodeStage.AntiCheat.ObscuredTypes;

public class Boss : MainBehavior,IHitable,IAttackable {
    public BossAction[] Actions;
    public BossData data;
    public IntRange attackCoolDown=new IntRange(0,0);
    public GameObject LineHitter,target,Shape;

    protected ObscuredString bossName;
    protected ObscuredFloat damage;
    protected ObscuredInt hitPoint;
    protected ObscuredFloat range;
    protected GameObject DmgPopUp;


    Transform aimer;
    GamePlayManager LC;
    Collider2D[] temp;
    Animator anim;
    Vector2 ChoosedDirection;
    Collider2D ttt;
    [SerializeField]
    bool right;
    bool Counter=true;
    bool InArea;
    float angle,time;
    int waitTime;
	// Use this for initialization
	public virtual void Start () {
        //Sorting The Actions In Order
        Sorting();
        RenewData();
        anim = GetComponent<Animator>();
        waitTime = attackCoolDown.Random;
        LC = GamePlayManager.instance;
        aimer = GameObject.FindWithTag("Leader").transform;
        DmgPopUp = Resources.Load("DmgPopUp", typeof(GameObject)) as GameObject;

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
         if (LC.gameState!=GamePlayState.Playing)
              return;
          if (!Counter)
              return;


        ttt = Physics2D.OverlapCircle(transform.position, range, CharacterLayer); 

        if (ttt)
        {
            if(!InArea)
            {
                LC.bgm.PlaySound(BGM.State.BossFight);
                InArea = true;
            }
        }
        else if (!ttt)
        {
            if (InArea)
            {
                LC.bgm.PlaySound(BGM.State.Main);
                InArea = false;
            }

        }

        
            temp = Physics2D.OverlapCircleAll(transform.position, range * 1.5f, MainBehavior.CharacterLayer);
        if (temp.Length > 0)
        {
            time += Time.deltaTime;
            if (time >= waitTime)
                ChooseAction();
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
    public virtual void ChooseAction()
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
                int a = Random.Range(0, 101);
                if (a <= 30)
                    Flip();
                anim.SetTrigger(Actions[i].ActionName);
                Counter = false;
                break;
            }
        }

    }
    void Flip()
    {

        Vector3 aa = Shape.transform.localScale;
        aa.x *= -1;
        Shape.transform.localScale = aa;
        right = !right;
    }

    public virtual void Splash()
    {
        Collider2D[] temp = Physics2D.OverlapCircleAll(transform.position, range, MainBehavior.CharacterLayer);
        
        foreach (var item in temp)
        {
            item.SendMessage("GetHit",(float) damage);
            Instantiate(DmgPopUp, item.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(damage.ToString(), DmgPopUpBehaivior.AttackType.EnemyAttack, item.transform.position);
        }
        Counter = true;
        waitTime = attackCoolDown.Random;
        time = 0;
    }
    public  void ChooseRandomDirection()
    {
        Vector2 dir = aimer.transform.position - LineHitter.transform.position;


         angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        LineHitter.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        ChoosedDirection = dir;

        ChoosedDirection = new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);

        if (ChoosedDirection.x > 0 && !right)
        {
            Flip();
        }
        else if (ChoosedDirection.x < 0 && right)
        {
            Flip();

        }

    }
    public virtual void AttackDirection()
    {
        temp = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, range * 2),angle, MainBehavior.CharacterLayer);
        foreach (var item in temp)
        {
            Vector2 dir = item.transform.position - transform.position;
            Vector2 t= new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);
            if (t == ChoosedDirection)
            {
                dir.Normalize();
                item.SendMessage("GetHit", (float)damage);
                Instantiate(DmgPopUp, item.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(damage.ToString(), DmgPopUpBehaivior.AttackType.EnemyAttack, item.transform.position);

                item.GetComponent<Rigidbody2D>().AddForce(dir *100);
            }
        }
        Counter = true;
        waitTime = attackCoolDown.Random;
        time = 0;

    }

    void OnDrawGizmos()
    {
       /* Gizmos.color = Color.red;
        DrawCube(LineHitter.transform.position, Quaternion.Euler(0, 0, angle), new Vector2(1, range * 2));*/
    }

    

    public virtual void GetHit(float dmg)
    {
        
            hitPoint -= (int)dmg;
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Die();
            }
        
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Attack()
    {
        ChooseAction();
        Counter = false;
    }

    public void AttackAnimation()
    {
        Counter = false;
    }

    public void AttackAllower()
    {
    }
}


[System.Serializable]
public class BossAction
{
    public string ActionName;
    
    public int chance;
}

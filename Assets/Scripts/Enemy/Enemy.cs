using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IsoMetricHandler))]
public class Enemy : MonoBehaviour,IHitable,IAttackable {
    public EnemyData data;
    public IntRange timeForMove=new IntRange(2,5);
    public GameObject centerPoint;
    public bool Gurdian;

    protected ObscuredFloat speedMultiPly;
    protected ObscuredFloat attackSpeed;
    protected ObscuredFloat hitPoint;
    protected ObscuredFloat damage;
    protected ObscuredFloat speed;
    protected ObscuredFloat range;
    protected ObscuredInt coin;
    protected Rigidbody2D rg;
    protected Animator anim;

    [SerializeField]
    GameObject coinObject;
    GameObject aim;
    LevelController levelController;
    protected Collider2D detectedCharacter;
    protected GameObject DmgPopUp;
    bool detect,move;
    protected float time;
    protected bool Attacking;
    GameObject text;
    Vector2 tt,direction;
    [HideInInspector]
    public Wave Father;
	// Use this for initialization
    public virtual void Start()
    {
        gameObject.layer = 0;
        levelController = LevelController.instance;
        aim = GameObject.FindWithTag("Aim");
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       // RenewData();
        if (!Gurdian)
            StartCoroutine(Move());
        DmgPopUp = Resources.Load("DmgPopUp", typeof(GameObject)) as GameObject;
        StartCoroutine(LayerChanger());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        detectedCharacter= Physics2D.OverlapCircle(centerPoint.transform.position, range, MainBehavior.CharacterLayer);
        

        


        time += Time.fixedDeltaTime;
        if (attackSpeed <= time && !Attacking && detectedCharacter)
        {
            AttackAnimation();
            move = false;
        }






        if (move & !Attacking)
        {
            rg.velocity = direction * speed;

        }
        anim.SetBool("Moving", move);

    }

    public void GetHit(float dmg)
    {
        hitPoint -= dmg;
        if (hitPoint < 0)
            Die();
    }

    void RenewData()
    {
        speedMultiPly = data.speed;
        attackSpeed = data.attackSpeed;
        speed = data.speed;
        hitPoint = data.hitPoint;
        damage = data.damage;
        damage = data.damage * ((Time.time / 20)+1);
        range = data.range;
        coin = data.coin * levelController.WorldCoinMultiply;
    }
    IEnumerator Move()
    {
        yield return new WaitUntil(() => detectedCharacter == null);

        yield return new WaitForSeconds(timeForMove.Random);
        IntRange a = new IntRange(0, 100);
        move = true;
        if (Vector2.Distance(transform.position, aim.transform.position) < 5)
        {

            if (a.Random <= 60)
            {
                Vector2 t = aim.transform.position - transform.position;
                t = t.normalized;
                // rg.velocity = t * speed;

                direction = t;

               // print("Follow");

            }
            else
            {
                //rg.velocity = GiveRandomMoveDiretion() * speed ;
                direction = GiveRandomMoveDiretion();
                //print("Random");

            }
        }
        else
        {
            Vector2 t = GiveRandomMoveDiretion();
            rg.velocity = t * speed;
            direction = t;
        }

        yield return new WaitForSeconds(timeForMove.Random);
        rg.velocity = Vector2.zero;
        move = false;


        StartCoroutine(Move());
    }
       
    


    public virtual void Attack()
    {

        if (detectedCharacter != null)
        {
            float f = (float)damage;
            detectedCharacter.SendMessage("GetHit", f);
            Instantiate(DmgPopUp, detectedCharacter.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(f.ToString(), DmgPopUpBehaivior.AttackType.EnemyAttack,detectedCharacter.gameObject.transform.position);
        }
        time = 0;
        Attacking = false;
    }


    public void Die()
    {
        LevelUIManager.Instance.MakeGoldBrust(transform.position);
        levelController.ChangeCoin(coin);
        KeyManager.Instance.ChangeKeyPart(1);
        Lean.Pool.LeanPool.Despawn(gameObject);
        Father.Decrease();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position,(Vector2)transform.position+( tt*4f));
    }

    public Vector2 GiveRandomMoveDiretion()
    {

        Vector2 t;
        bool find = false;
        do
        {
            t = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            RaycastHit2D a = Physics2D.Raycast(transform.position, t, 4f,1<<11);
            tt = t;
            if (a.collider==null)
                find = true;

        } while (find==false);
        tt = t;

        return t ;
    }

    void Reset()
    {
        GetComponent<Rigidbody2D>().mass = 100;
        GetComponent<Rigidbody2D>().angularDrag = 6.6f;
        GetComponent<Rigidbody2D>().drag = 100;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<IsoMetricHandler>().Center = gameObject;
    }
    public IEnumerator LayerChanger()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.layer = 8;
    }

    public void AttackAnimation()
    {
        anim.SetTrigger("Attack");
        Attacking = true;
    }

    public void AttackAllower()
    {
        Attacking = false;
        time = 0;
    }
}



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
    public bool right;

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
    GamePlayManager GPM;
    protected Collider2D detectedCharacter;
    protected GameObject DmgPopUp;
    bool detect,move;
    protected float time;
    protected bool Attacking=false;
    GameObject text;
    Vector2 tt,direction,diffrences;
    [HideInInspector]
    public bool released;
	// Use this for initialization
    public virtual void Start()
    {
        gameObject.layer = 15;
        GPM = GamePlayManager.instance;
        aim = GameObject.FindWithTag("Aim");
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        RenewData();
        DmgPopUp = Resources.Load("DmgPopUp", typeof(GameObject)) as GameObject;
        StartCoroutine(Entrance());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (released == false)
            return;

        if (GPM.gameState != GamePlayState.Playing)
        {
            rg.velocity = Vector2.zero;
            return;
        }


        detectedCharacter= Physics2D.OverlapCircle(centerPoint.transform.position, range, MainBehavior.CharacterLayer);


        if (detectedCharacter)
        {
            diffrences = detectedCharacter.transform.position - transform.position;
            if (Vector2.Distance(transform.position, detectedCharacter.transform.position) > 0.5f)
            {
                if (diffrences.x > 0 && !right)
                    Flip();
                else if (diffrences.x < 0 && right)
                    Flip();
            }

        }
        else
        {
            if (direction.x > 0 && !right)
                Flip();
            else if (direction.x < 0 && right)
                Flip();
        }
        

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
        attackSpeed = data.attackSpeed*GPM.WorldAttackMultiPly;
        speed = data.speed;
        hitPoint = data.hitPoint;
        damage = data.damage*GPM.EnemyDamageMultiPly;
        range = data.range;
        coin =(int)( data.coin * GPM.WorldCoinMultiply);
    }
    IEnumerator Move()
    {
        yield return new WaitUntil(() => detectedCharacter == null);

        yield return new WaitForSeconds(timeForMove.Random);
        move = true;


        Vector2 t = Camera.main.transform.position - transform.position;
        t = t.normalized;

        direction = t;

        yield return new WaitForSeconds(timeForMove.Random);
        rg.velocity = Vector2.zero;
        move = false;


        StartCoroutine(Move());
    }
       
    public IEnumerator Entrance()
    {
        Vector2 direction = Vector2.zero - (Vector2)transform.position;
        direction.Normalize();
        Vector2 Orgin = transform.position;
        if (direction.x > 0 && !right)
            Flip();
        else if (direction.x < 0 && right)
            Flip();

        anim.SetBool("Moving", true);


        while (Vector2.Distance(transform.position,Orgin)<=11)
        {
            rg.velocity = direction * (5);
            time += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("Moving", false);


        released = true;
        LayerChanger();
        StartCoroutine(Move());
    }


    public virtual void Attack()
    {

        if (detectedCharacter != null)
        {
            float f = (float)damage;
            detectedCharacter.SendMessage("GetHit", f);
            Instantiate(DmgPopUp, detectedCharacter.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(((int)f).ToString(), DmgPopUpBehaivior.AttackType.EnemyAttack,detectedCharacter.gameObject.transform.position);
        }
        time = 0;
        Attacking = false;
    }


    public void Die()
    {
        if (GPM.gameState != GamePlayState.Finish)
        {
            GPM.ChangeCoin(coin);
            XpController.Instance.AddXp(coin);
            // KeyManager.Instance.ChangeKeyPart(1);
            GameAnalyticsManager.SendCustomEvent("Enemy:" + data.enemyName);
        }
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position,(Vector2)transform.position+( tt*4f));
    }
    void Flip()
    {

        Vector3 aa = transform.localScale;
        aa.x *= -1;
        transform.localScale = aa;
        right = !right;
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
        gameObject.layer = 15;

    }
    public void LayerChanger()
    {
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



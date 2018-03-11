using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour,IHitable,IAttackable {
    public EnemyData data;
    public IntRange timeForMove;
    public int powerRate;
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
    GameObject coinObject,aim;
    LevelController levelController;
    public Collider2D detectedCharacter;
    bool detect,move;
    protected float time;

    [HideInInspector]
    public bool Attacking;
    Vector2 tt,direction;
	// Use this for initialization
    public virtual void Start()
    {
        gameObject.layer = MainBehavior.BlocksLayer.value;
        levelController = LevelController.instance;
        aim = GameObject.FindWithTag("Aim");
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        RenewData();
        if (!Gurdian)
            StartCoroutine(Move());
        StartCoroutine(LayerChanger());
    }
	
	// Update is called once per frame
    void Update()
    {


        detectedCharacter = Physics2D.OverlapCircle(transform.position, range, MainBehavior.CharacterLayer);



        if (detectedCharacter)
        {
            time += Time.deltaTime;
            if (attackSpeed <= time&&!Attacking)
                Attack();
            move = false;
        }
        else
            time = 0;
        



        
        if (move)
        {
            rg.velocity = direction* speed;
        }


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
        detectedCharacter.SendMessage("GetHit",(float) damage);

        time = 0;
    }


    public void Die()
    {
       // Instantiate(coinObject, transform.position, Quaternion.identity).transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = coin.ToString();
        LevelUIManager.Instance.MakeGoldBrust(transform.position);
        levelController.ChangeCoin(coin);
        KeyManager.Instance.ChangeKeyPart(1);
        Destroy(gameObject);
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
    }
    public IEnumerator LayerChanger()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.layer = MainBehavior.EnemyLayer.value;
    }

    public void AttackAnimation()
    {
    }

    public void AttackAllower()
    {
    }
}



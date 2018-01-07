using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Enemy : MonoBehaviour,IHitable,IAttackable {
    public EnemyData data;
    public IntRange timeForMove;
    public bool Gurdian;

    protected ObscuredFloat speedMultiPly;
    protected ObscuredFloat attackSpeed;
    protected ObscuredFloat hitPoint;
    protected ObscuredFloat damage;
    protected ObscuredFloat speed;
    protected ObscuredFloat range;
    protected ObscuredInt coin;
    protected Rigidbody2D rg;


    [SerializeField]
    GameObject coinObject,aim;
    LevelController levelController;
    LayerMask charactersLayer;
    Collider2D detectedCharacter;
    bool detect;
    float time;


    Vector2 tt;
	// Use this for initialization
	public virtual void Start () {
        levelController = LevelController.instance;
        aim = GameObject.FindWithTag("Aim");
        rg = GetComponent<Rigidbody2D>();
        charactersLayer  = 1 << 10;

        RenewData();
        StartCoroutine(Move());
	}
	
	// Update is called once per frame
	void Update () {
        time+=Time.deltaTime;


        if (Vector2.Distance(transform.position, aim.transform.position) < range)
        {
            rg.bodyType = RigidbodyType2D.Static;
            rg.velocity = Vector2.zero;
        }
        else
            rg.bodyType = RigidbodyType2D.Dynamic;

        detectedCharacter=Physics2D.OverlapCircle(transform.position,range,charactersLayer);

        if (detectedCharacter && attackSpeed <= time)
            Attack();

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
        yield return new WaitForSeconds(timeForMove.Random);
        IntRange a = new IntRange(0, 100);
        if(Vector2.Distance(transform.position, aim.transform.position) < 5)
        {

            if (a.Random <= 60)
            {
                Vector2 t = aim.transform.position - transform.position;
                t = t.normalized;
                rg.velocity = t * speed;
                print("Follow");


            }
            else
            {
                rg.velocity = GiveRandomMoveDiretion() * speed ;
                print("Random");

            }

        }
        else
        {
            Vector2 t = GiveRandomMoveDiretion();
            rg.velocity = t * speed;
            print("Random "+t+ " velo "+rg.velocity);
        }

        yield return new WaitForSeconds(timeForMove.Random);
        rg.velocity = Vector2.zero;
        

        StartCoroutine(Move());

    }

    public virtual void Attack()
    {
        detectedCharacter.SendMessage("GetHit",(float) damage);

        time = 0;
    }


    public void Die()
    {
        Instantiate(coinObject, transform.position, Quaternion.identity).transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = coin.ToString();
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

            print("Choosing ... ");
        } while (find==false);
        tt = t;
        print("Choosed "+tt);

        return t ;
    }
}



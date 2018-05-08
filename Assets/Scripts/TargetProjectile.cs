using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectile : MainBehavior {

    public float speed, diffrenceAngular;


    Rigidbody2D rg;
    float  damage;
    public Vector2 target;
    bool shoot;
    Vector2 t;
    // Use this for initialization
    void Start()
    {
        t = target -(Vector2) transform.position;
        t.Normalize();

        rg = GetComponent<Rigidbody2D>();


        var dir = target -(Vector2) transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= diffrenceAngular;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

       
    }

    // Update is called once per frame
    void Update()
    {
        

        t = target- (Vector2)transform.position;
        t.Normalize();


       

        var dir = target- (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= diffrenceAngular;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        rg.velocity = transform.up * -1 * speed;
        if (Vector2.Distance(transform.position, target) < 0.3f)
            Die();

    }
    public void Spawn(Vector2 t, float d)
    {
        target = t;
        damage = d;
        shoot = true;
    }

    public void GetHit(float dmg)
    {
        throw new System.NotImplementedException();
    }
    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.layer == 10)
        {
            target.SendMessage("GetHit", damage);
            Destroy(gameObject);
        }

    }
    public void Die()
    {
        Destroy(gameObject);
    }
}

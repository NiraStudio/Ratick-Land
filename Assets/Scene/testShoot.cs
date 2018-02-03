using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShoot : MonoBehaviour {
    public GameObject bullet,traget;
    Vector2 t;
    float qq=3;
    float a = 1;
    Rigidbody2D rg;
    public float s=5;
    float maxRange, range;
    bool aaa;
	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody2D>();
      //  rg.AddForce(Vector2.up * 100);
       // maxRange = Vector2.Distance(transform.position, target.transform.position);
        qq = s;
        maxRange = qq;
	}

   /* void FixedUpdate()
    {
        range = Vector2.Distance(transform.position, target.transform.position);
         t = target.transform.position - transform.position;
        t.Normalize();
        if (range < maxRange / 3) {
            t = t * 10;
            t += Vector2.up * 10;
        }
        else
        {
            t = t * 10;
        }
       
        rg.AddForce(t*10 , ForceMode2D.Force);
    }*/
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            shoot();
        if (aaa)
            GetComponent<Rigidbody2D>().velocity = Vector2.right * 300;
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
    void shoot()
    {
        aaa = !aaa;
    }
    IEnumerator changeY()
    {
        do
        {
            qq += 0.1f*Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        } while (qq<1);

        do
        {
            qq -= 0.1f*Time.deltaTime;
            yield return new WaitForSeconds(0.02f);
        } while (qq >0);
        qq = 0;
    }
}

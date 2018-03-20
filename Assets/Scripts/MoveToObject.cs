using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour {
    public Transform target;

    Rigidbody2D rg;
	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Leader").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector2 tt = (target.transform.position - transform.position);
            tt.Normalize();

            if (Vector2.Distance(transform.position, target.transform.position) > 0.2f)
                rg.velocity = tt * 3;
            else
                rg.velocity = Vector2.zero;
        }
        else
            rg.velocity = Vector2.zero;
    }
}

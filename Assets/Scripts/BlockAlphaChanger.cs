using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAlphaChanger : MainBehavior {
    public float Distance;
    public SpriteRenderer sp;

    Color normal, temp;
    Collider2D hit;
    Vector2 size;
	// Use this for initialization
	void Start () {
        if (sp == null)
            sp = GetComponent<SpriteRenderer>();

        normal = sp.color;
        temp = normal;
        temp.a = .5f;
        print(sp.bounds.extents.y);
        Distance = sp.bounds.extents.y;
        size=new Vector2(1, Distance * 1.5f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        hit = Physics2D.OverlapBox(sp.bounds.center, size, 0, CharacterLayer);
        if(!hit)
            hit = Physics2D.OverlapBox(sp.bounds.center, size, 0, EnemyLayer);

        if (hit)
        {
            sp.color = temp;
        }
        else
            sp.color = normal;


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 t = transform.position;
        
        Gizmos.DrawWireCube(sp.bounds.center,size );
    }
}

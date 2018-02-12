using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoMetricHandler : MainBehavior {
    public bool Static=true;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	 public virtual void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
      //  spriteRenderer.sortingOrder = (-(int)(transform.position.y * 100));
        Vector3 t = transform.position;
        t.z = (transform.position.y);
        transform.position = t;
	}
	
	// Update is called once per frame
     void Update()
     {
         if (Static)
             return;
         ChangeByTransform(transform);
     }
     public static int giveSortingOrderNumber(float y)
     {
       return  (-(int)(y * 100));
     }
     public static void ChangeByTransform(Transform trans)
     {
         Vector3 t = trans.position;
         t.z = (t.y) ;
         trans.position = t;
     }
}

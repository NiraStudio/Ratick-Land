using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoMetricHandler : MainBehavior {
    public bool Static=true;
    public GameObject Center;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	 public virtual void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
      //  spriteRenderer.sortingOrder = (-(int)(transform.position.y * 100));
        if (Center == null)
            Center = gameObject;
        Vector3 t = transform.position;
        t.z = (Center.transform.position.y);
        transform.position = t;
	}
	
	// Update is called once per frame
     void LateUpdate()
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

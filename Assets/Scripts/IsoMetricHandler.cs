using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoMetricHandler : MainBehavior {
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	 public virtual void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (-(int)(transform.position.y * 100));
	}
	
	// Update is called once per frame

     public static int giveSortingOrderNumber(float y)
     {
       return  (-(int)(y * 100));
     }
}

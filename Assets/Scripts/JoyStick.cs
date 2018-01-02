using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MainBehavior
{
    public GameObject handle;
    public bool spriteMethod;
    public float distance;
    public float speed;
    public Vector3 direction;
    Vector2 t;
    Vector2 mousePos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (spriteMethod)
            SpriteMethod();
        else
            UiMethod();
	}
    void SpriteMethod() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(transform.position, mousePos) <= distance)
            handle.transform.position = mousePos;
        else
        {
            t = mousePos - (Vector2)transform.position;
            t = t.normalized;
            handle.transform.position = (Vector2)transform.position + t * distance;
        }
        speed = (handle.transform.position - transform.position).magnitude / distance;
        direction = (handle.transform.position - transform.position).normalized;
    
    }
    void UiMethod()
    {
        mousePos = Input.mousePosition;
        if (Vector2.Distance(transform.position, mousePos) <= distance)
            handle.transform.position = mousePos;
        else
        {
            t = mousePos - (Vector2)transform.position;
            t = t.normalized;
            handle.transform.position = (Vector2)transform.position + t * distance;
        }
        speed = (handle.transform.position - transform.position).magnitude / distance;
        direction = (handle.transform.position - transform.position).normalized;
    
    }
}

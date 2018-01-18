using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public GameObject entry;
    public Vector2 entrySize;
    public GameObject image;

   public LayerMask a;
	// Use this for initialization
	void Start () {
        a = 1 >> 10;
	}
	
	// Update is called once per frame
	void LateUpdate () {
       /* bool enter = Physics2D.OverlapBox(entry.transform.position, entrySize, 0,a);
        if (enter)
        {
            image.SetActive(true);
            print("true");
        }
        else
        {
            image.SetActive(false);
            print("false");

        }*/
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(entry.transform.position, entrySize);
    }
}

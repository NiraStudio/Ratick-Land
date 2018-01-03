using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    public bool destroy;
    public float time;
	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitUntil(() => destroy == true);
        Destroy(gameObject, time);
	}
	
	// Update is called once per frame
    public void DestroyGameObject()
    {
        Destroy(gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsScript : MonoBehaviour {
    public IntRange WaitTime;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        StartCoroutine(startFly());
    }

    // Update is called once per frame
    IEnumerator startFly()
    {
        yield return new WaitForSeconds(WaitTime.Random);
        anim.SetTrigger("Move");
        StartCoroutine(startFly());
    }
}

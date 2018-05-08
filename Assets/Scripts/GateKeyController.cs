using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyController : MonoBehaviour,IHitable {
    public Wave wave;

    public void Die()
    {
        throw new NotImplementedException();
    }

    public void GetHit(float dmg)
    {
        if (!wave.Open)
            wave.Spawn();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommingSoonSceneBehaivior : MonoBehaviour {
    public ParticleSystem CoalParticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void EmitParticle()
    {
        CoalParticle.Emit(15);
    }
}

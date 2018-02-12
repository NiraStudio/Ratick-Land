using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBrust : MonoBehaviour {
    public enum State
    {
        Brust,Wait,Move
    }


    
     
    public ParticleSystem PS;
    public State state;
    public Transform Pos;
    public float brustTime, WaitTime;

    ParticleSystem.Particle[] particles;
    Vector2[] poses;
	// Use this for initialization

	void Start () {
        state = State.Wait;
        var a = PS.emission;
        a.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Brust:
               
                break;
            case State.Move:
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].position = Vector2.MoveTowards(particles[i].position, Pos.position, 10 * Time.deltaTime);
                }
                PS.SetParticles(particles, particles.Length);
                break;
        }

        
	}
    public void make(Transform Target,int Amount)
    {
        PS.Emit(Amount);
        particles = new ParticleSystem.Particle[PS.main.maxParticles];
        poses = new Vector2[particles.Length];
        PS.GetParticles(particles);
        for (int i = 0; i < particles.Length; i++)
        {
            poses[i] =(Vector2) gameObject.transform.position + (Random.insideUnitCircle * 0.5f);
        }
        Pos = Target;
        state = State.Brust;
        StartCoroutine(wait(2));
        Destroy(gameObject,5);
    }

    IEnumerator wait(float t)
    {
        yield return new WaitForSeconds(brustTime);
        var a = PS.main;
        a.simulationSpeed = 0;


        state = State.Wait;

        yield return new WaitForSeconds(WaitTime);
        PS.GetParticles(particles);
        state = State.Move;
    }
}

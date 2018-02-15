using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mehdi : MonoBehaviour {
    public UnityEvent events;
	// Use this for initialization
	void Start () {
        events.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void A()
    {
        print("Hello");
    }
    public void B()
    {
        print("World");

    }
}

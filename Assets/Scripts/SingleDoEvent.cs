using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleDoEvent : MonoBehaviour {
    public UnityEvent Action;

	// Use this for initialization
	public void DoAction()
    {
        Action.Invoke();
    }

}

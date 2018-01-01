using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MainBehavior {
    public static GameManager instance;
	// Use this for initialization

    void Awake()
    {
        instance = this;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class MainData
{
    public Dictionary<int, int> charactersData;
}



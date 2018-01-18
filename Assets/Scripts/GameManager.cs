using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MainBehavior {
    public static GameManager instance;
    public SlotContainer SlotData
    {
        get { return Slot; }
        set { Slot = value; }
    }

    public SlotContainer Slot;
	// Use this for initialization

    void Awake()
    {
        instance = this;
    }
	void Start () {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}

public class MainData
{
    public Dictionary<int, int> charactersData;
}



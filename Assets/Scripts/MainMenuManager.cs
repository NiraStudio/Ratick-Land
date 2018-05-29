using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MainBehavior {
    public static MainMenuManager Instance;

    GameManager GM;
    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        GM = GameManager.instance;
        LoadingScreenManager.Instance.Open();
    }

    public void Play()
    {
        GoToArmyScene();
        print("Here");
    }
    
    public void GoToArmyScene()
    {
        GoToScene("ArmyScene");
    }
    // Update is called once per frame

}

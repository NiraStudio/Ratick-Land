using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MainBehavior {
    public MainMenuCamera MMC;
    public UnityEngine.Events.UnityEvent PreGamePlay;
    GameManager GM;
    
    float coinTemp,lerp;
	// Use this for initialization
	void Start () {
        print("here");
        GM = GameManager.instance;
        OpenScreen();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Play();
    }
	public void Play()
    {
        if(MMC.CurrentState==MainMenuCamera.CameraPos.Main)
        {
            StartCoroutine(GoToPlay());
        }
    }
    IEnumerator GoToPlay()
    {
        MMC.Allow = false;
        MMC.SendToSky();
        //swordAnimation
        PreGamePlay.Invoke();
        yield return new WaitForSeconds(2);
        GoToScene("SlotContainer");
    }
	// Update is called once per frame
	
}

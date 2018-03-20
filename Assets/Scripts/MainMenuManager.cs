using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MainBehavior {
    public static MainMenuManager Instance;

    public MainMenuCamera MMC;
    public UnityEngine.Events.UnityEvent PreGamePlay;
    GameManager GM;
    void Awake()
    {
        Instance = this;
    }
    float coinTemp,lerp;
	// Use this for initialization
	void Start () {

        GM = GameManager.instance;
        if(!GM.sfx.IsPlaying("MainMenu"))
        GM.sfx.PlaySound("MainMenu");
        if (PlayerPrefs.GetFloat("BossKilled") == 1)
        {
            InformationPanel.Instance.OpenInfoPanel( "تو خرس رو کشتی \n  کد رو تا رو انتشار اصلی بازی نگه دار"+"\n"+GM.giveeRandomRewardCode(),"You killed the Bear \n Keep this code till the game release\n"+GM.giveeRandomRewardCode(),false, () =>
            {
                PlayerPrefs.SetInt("FirstBoss", 0);
                PlayerPrefs.SetInt("BossKilled", 0);
            }, "OK");
            
        }
        if(PlayerPrefs.GetInt("Tutorial")==1)
        TutorialManager.Instance.OpenStep("Tut_1");
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

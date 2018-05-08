using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInput : MonoBehaviour {


    #region Singleton
    public static GamePlayInput Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject aimer;
    public JoyStick JS;
    public float speed;
    public Vector2 Direction;
    public bool Move=true;
    Vector2 t;
    Touch[] touches;
    SFX sfx;
    GamePlayManager LC;
    Rigidbody2D aimerRG;
	// Use this for initialization
	void Start () {
        sfx = GetComponent<SFX>();
        LC = GetComponent<GamePlayManager>();

        aimerRG = aimer.GetComponent<Rigidbody2D>();
        JoyStickTurnOff();
	}

    // Update is called once per frame
    void Update()
    {

        if (LC.gameState == GamePlayState.Finish)
            return;

        #region inputs
        if (Application.isMobilePlatform)
        {
            
            touches = Input.touches;
            foreach (var item in touches)
            {
                print(item.tapCount);
            }
            if (touches.Length == 1)
                switch (touches[0].phase)
                {
                    case TouchPhase.Began:
                        if (touches[0].tapCount ==1)
                            JoyStickTurnOn(Camera.main.ScreenToWorldPoint(touches[0].position));
                        break;
                    case TouchPhase.Ended:
                        if (touches[0].tapCount >= 2)
                            GatherCharacters();
                        JoyStickTurnOff();
                        break;
                }
        }
        else if (Application.isEditor)
        {

            touches = Input.touches;

            if (touches.Length == 1)
            {
                switch (touches[0].phase)
                {
                    case TouchPhase.Began:
                        if (touches[0].tapCount == 1)
                            JoyStickTurnOn(Camera.main.ScreenToWorldPoint(touches[0].position));
                        break;
                    case TouchPhase.Ended:
                        if (touches[0].tapCount >= 2)
                            GatherCharacters();
                        JoyStickTurnOff();
                        break;
                }
            }

            if (Input.GetMouseButtonDown(0))
                JoyStickTurnOn(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetMouseButtonUp(0))
                JoyStickTurnOff();
        }
        #endregion

        Direction = JS.direction;

        /* //Aimer Moves
         #region LeaderMove
         if (Move)
         {
             Direction = JS.direction;
             aimerRG.velocity = Direction * speed;
         }
         else
             aimer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
         #endregion
     */
    }
    public void GatherCharacters()
    {
        Character[] c = GameObject.FindObjectsOfType<Character>();
        foreach (var item in c)
        {
            print(item.data.characterName);
            item.StartGathering();
        }
    }
    public void JoyStickTurnOn(Vector2 pos)
    {
        JS.gameObject.SetActive(true);
        JS.transform.position = pos;
        Move = true;
        sfx.PlaySound("Walking");
        aimer.transform.position = Camera.main.transform.position;
        for (int i = 0; i < aimer.transform.childCount; i++)
        {
            aimer.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void JoyStickTurnOff()
    {
        JS.gameObject.SetActive(false);
        Move = false;
        sfx.StopSound("Walking");
        for (int i = 0; i < aimer.transform.childCount; i++)
        {
            aimer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    

}

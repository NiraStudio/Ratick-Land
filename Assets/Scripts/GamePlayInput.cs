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
    Rigidbody2D leaderRG;
    SFX sfx;
	// Use this for initialization
	void Start () {
        sfx = GetComponent<SFX>();
	}
	
	// Update is called once per frame
	void Update () {
        #region inputs
        if (Application.isMobilePlatform)
        {
            touches = Input.touches;
            if (touches.Length == 1)
                switch (touches[0].phase)
                {
                    case TouchPhase.Began:
                        JoyStickTurnOn(Camera.main.ScreenToWorldPoint(touches[0].position));
                        break;
                    case TouchPhase.Ended:
                        JoyStickTurnOff();
                        break;
                }
        }
        else if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
                JoyStickTurnOn(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetMouseButtonUp(0))
                JoyStickTurnOff();
        }
        #endregion


        //Aimer Moves
        #region LeaderMove
        Direction = JS.direction;
        if (Move)
        {
            t = aimer.transform.position;
            t.x += Direction.x * (JS.speed * speed) * Time.deltaTime;
            t.y += Direction.y * (JS.speed * speed) * Time.deltaTime;
            aimer.transform.position = t;
        }

        #endregion
    }
    void JoyStickTurnOn(Vector2 pos)
    {
        JS.gameObject.SetActive(true);
        JS.transform.position = pos;
        Move = true;
        sfx.PlaySound("Walking");

    }
    void JoyStickTurnOff()
    {
        JS.gameObject.SetActive(false);
        Move = false;
        sfx.StopSound("Walking");

    }

}

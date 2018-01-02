﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MainBehavior
{
    public static LevelController instance;
    public CharacterDataBase characterDataBase;
    public CameraController cameraController;
    public JoyStick joyStick;
    public GameObject aimer;
    public float speedMultiPly;
    public bool Move
    {
        get { return move; }
    }


    Dictionary<int, int> data = new Dictionary<int, int>();
    bool move;
    /// cach var
    Vector2 t;
    Touch[] touches;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        data.Add(1, 5);
        data.Add(2, 3);
        spawnCharacters();
    }

    // Update is called once per frame
    void Update()
    {
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

        #region characterMoves
        if (move)
        {

            t = aimer.transform.position;
            t.x += joyStick.direction.x * (joyStick.speed * speedMultiPly) * Time.deltaTime;
            t.y += joyStick.direction.y * (joyStick.speed * speedMultiPly) * Time.deltaTime;
            aimer.transform.position = t;

        }
        #endregion
    }
    void JoyStickTurnOn(Vector2 pos)
    {
        joyStick.gameObject.SetActive(true);
        joyStick.transform.position = pos;
        move = true;
    }
    void JoyStickTurnOff()
    {
        joyStick.gameObject.SetActive(false);
        move = false;
    }
    void spawnCharacters()
    {
        foreach (var item in data)
        {
            CharacterData c = characterDataBase.GiveByID(item.Key);
            switch (c.type)
            {
                case CharacterData.Type.Soldier:
                    for (int i = 0; i < item.Value; i++)
                    {
                        GameObject b = Instantiate(c.prefab);
                        b.transform.position = Random.insideUnitCircle * 3;
                        cameraController.AddTarget(b);
                        b.GetComponent<Character>().Release(true);
                    }

                    break;
                case CharacterData.Type.Hero:
                    GameObject a = Instantiate(c.prefab);
                    a.transform.position = Random.insideUnitCircle * 3;
                    cameraController.AddTarget(a);
                    a.GetComponent<Character>().Release(true);
                    break;

            }
        }
        
    }

    public void AddCharacter()
    {
        GameObject g = Instantiate(characterDataBase.GiveByID(1).prefab);
        g.transform.position = Random.insideUnitCircle * 3;
        cameraController.AddTarget(g);
        g.GetComponent<Character>().Release(true);

    }

}

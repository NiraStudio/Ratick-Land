using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;


public class MissionTextBehaivior : MonoBehaviour {
    public LocalizedDynamicText text;
    public ParticleSystem PS;
    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MakeText(string Persian,string English)
    {
        text.ChangeText(Persian, English, false, false);
        gameObject.SetActive(true);
    }
    public void Finish()
    {
        gameObject.SetActive(false);
        GamePlayManager.instance.gameState = GamePlayState.Playing;
    }
}

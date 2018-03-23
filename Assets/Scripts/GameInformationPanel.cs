using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformationPanel : MonoBehaviour {

    bool open;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenAndClose()
    {
        anim.SetTrigger(open ? "Close" : "Open");
        open = !open;
    }
    public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }
}

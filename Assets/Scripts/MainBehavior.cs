using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GoToScene(string SceneName)
    {
        LoadingScreenManager.Instance.GoToScene(SceneName);
    }
    public void OpenScreen()
    {
        LoadingScreenManager.Instance.Open();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{

    #region Singleton
    public static LoadingScreenManager Instance;
    void Awake()
    {
        Instance = this;
    }

    #endregion

    public string OpenId, CloseId;
    public GameObject DisableSceen;
    string SceneName;
    int SceneId=-1;
    Animator anim;
    SFX sfx;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<SFX>();
    }
    
    // Update is called once per frame
    public void Open()
    {
        anim.SetTrigger(OpenId);
        sfx.PlaySound("Stone");
        DisableSceen.SetActive(false);
        print("Open Loading");
    }
    public void GoToScene(string SceneName)
    {
        DisableSceen.SetActive(true);
        anim.SetTrigger(CloseId);
        sfx.PlaySound("Stone");
        this.SceneName = SceneName;
    }
    public void GoToScene(int SceneId)
    {
        DisableSceen.SetActive(true);
        anim.SetTrigger(CloseId);
        sfx.PlaySound("Stone");
        this.SceneId = SceneId;
    }
    public void SceneChange()
    {
        if (SceneId >= 0)
        {
            SceneManager.LoadScene(SceneId);
            SceneId = -1;
        }
        else if (SceneName != null)
        {
            SceneManager.LoadScene(SceneName);
            SceneName = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyManager : MonoBehaviour {

    public static SurveyManager Instance;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

    }



    public List<string> data = new List<string>();
    public GameObject First, Yes, No,LastScene;
    // Use this for initialization
    Animator anim;
    void Stat()
    {
        gameObject.SetActive(false);
    }
    public void GoToQScene(bool yes)
    {
        First.SetActive(false);
        if (yes)
        {
            Yes.SetActive(true);
            GameAnalyticsManager.SendCustomEvent("Survey:Like The Game");
        }
        else
        {
            No.SetActive(true);
            GameAnalyticsManager.SendCustomEvent("Survey:Dont Like The Game");

        }

    }
    public void GiveGift()
    {
        RewardInfo r = RewardManager.Instance.MakeReward(RewardType.Coin, 1000);
        RewardManager.Instance.AddReward(r);
        InformationPanel.Instance.OpenRewardPanel(r, null, "");

    }
    public void SendData()
    {
        foreach (var item in data.ToArray())
        {
            GameAnalyticsManager.SendCustomEvent(item);
        }
            No.SetActive(false);
            Yes.SetActive(false);
        LastScene.SetActive(true);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        GetComponent<Animator>().SetTrigger("Close");
        PlayerPrefs.SetInt("Played", PlayerPrefs.GetInt("Played") + 1);

    }
    public void SetOff()
    {
        gameObject.SetActive(false);
    }
}

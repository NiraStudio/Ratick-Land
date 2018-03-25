using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour {
    public GameObject negative;
    public Animator AdChestAnimation;
    public AdScript adBtn;
    #region Singleton
    public static ShopMenuManager Instance;

    void Awake()
    {
        Instance = this;
    }
    #endregion

    bool canBuy;
    InformationPanel IP;
    // Use this for initialization
    void Start () {
        IP = InformationPanel.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        negative.SetActive(adBtn.HaveAd);
        AdChestAnimation.SetBool("Move", adBtn.HaveAd);
	}
    public void CheckCoinCost(int cost)
    {
        if (GameManager.instance.coinAmount < cost)
        {
            InformationPanel.Instance.OpenInfoPanel("سکه کافی نداری", "You dont have enough Coin", PanelColor.Alert,false, () => { });
            canBuy = false;
        }
        else
        {
            canBuy = true;
        }
    }
    public void OpenChest(string ChestName)
    {
        if (!canBuy)
            return;
        ChestManager.Instance.OpenChest(ChestName);
    }
}

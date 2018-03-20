using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TapsellSDK;

public class AdScript :MainBehavior
{
    public string AdName;
    public string ZoneID;
    public AdByType[] adsType = new AdByType[System.Enum.GetValues(typeof(RewardType)).Length];
    public bool HaveAd,GiveReward;
    public UnityEvent Extra;
    TapsellAd ad;
    RewardManager RM;
	// Use this for initialization
	void Start () {

        findAd();
        RM = RewardManager.Instance ;
	}
	
	// Update is called once per frame
	void Update () {
        if (HaveAd==true)
            GetComponent<Button>().interactable = true;
        else
            GetComponent<Button>().interactable = false;


	}
    
    void findAd()
    {

        Tapsell.requestAd(ZoneID, true,
    (TapsellAd result) =>
    {
        // onAdAvailable
        Debug.Log("Action: onAdAvailable");
        ad = result; 
        HaveAd = true;
       
    },

    (string zoneId) =>
    {
        // onNoAdAvailable
        Debug.Log("No Ad Available");
        HaveAd = false;

    },

    (TapsellError error) =>
    {
        // onError
        Debug.Log(error.error);
        HaveAd = false;

    },

    (string zoneId) =>
    {
        // onNoNetwork
        Debug.Log("No Network");
        HaveAd = false;

    },

    (TapsellAd result) =>
    {
        // onExpiring
        Debug.Log("Expiring");
        HaveAd = false;

        // this ad is expired, you must download a new ad for this zone
    }
);
    }
    public void SHowAd()
    {
        if (!HaveAd)
            return;
        TapsellShowOptions showOptions = new TapsellShowOptions();
        showOptions.backDisabled = true;
        showOptions.immersiveMode = false;
        showOptions.rotationMode = TapsellShowOptions.ROTATION_LOCKED_REVERSED_PORTRAIT;
        showOptions.showDialog = true;
        RewardInfo r=new RewardInfo();
        Tapsell.setRewardListener((TapsellAdFinishedResult result) =>
        {
            if (GiveReward)
            {
                r = GiveRandom();
                RM.AddReward(r);
                InformationPanel.Instance.OpenRewardPanel(r, null, "Awesome");
            }
            Extra.Invoke();
            ad = null;
            HaveAd = false;
            findAd();
            GameAnalyticsManager.SendCustomEvent("AD Watched");
        }
);
        Tapsell.showAd(ad, showOptions);
        

    }

    RewardInfo GiveRandom()
    {
        RewardInfo r = new RewardInfo();

        List<AdByType> t = new List<AdByType>();
        AdByType aa=new AdByType();
        foreach (var item in adsType)
        {
            if (item.Allow == true)
                t.Add(item);
        }
        if (t.Count > 0)
            aa = t[Random.Range(0, t.Count)];

        r = RM.MakeReward(aa.type, aa.Amount.Random);

        return r;
    }


    [System.Serializable]
    public class AdByType
    {
        public bool Allow;
        public RewardType type;
        public IntRange Amount;
    }
}

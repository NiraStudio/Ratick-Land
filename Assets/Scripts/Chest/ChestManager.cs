using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour {
    public static ChestManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public RewardInfo AdReward;
    Button BTN;
    public ChestType[] chests;
	// Use this for initialization
	void Start () {
        BTN = GetComponent<Button>();
        Close();
	}
    
	
	// Update is called once per frame
	public void OpenChest (string Name)
    {
        gameObject.SetActive(true);
        foreach (var item in chests)
        {
            if (item.Type == Name)
            {
                item.chest.MakeChest();
                item.chest.gameObject.SetActive(true);
                BTN.onClick.RemoveAllListeners();
                BTN.onClick.AddListener(item.chest.Animation.buttonDown);
                GameAnalyticsManager.SendCustomEvent(Name);
            }
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
    public void GetAdReward()
    {
        RewardManager.Instance.AddReward(AdReward);
    }


    [System.Serializable]
    public class ChestType
    {
        public string Type;
        public Chest chest;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour {

    public int RewardCount;
    public RewardStates[] rewardState;
    public RewardStates[] AdState;
    List<RewardInfo> rewards = new List<RewardInfo>();
    RewardInfo AdReward;
	// Use this for initialization
    public void MakeChest()
    {
        RewardStates aa = new RewardStates();
        for (int i = 0; i < rewardState.Length - 1; i++)
            for (int j = 0; j < rewardState.Length - 1; j++)
                if (rewardState[j].chance > rewardState[j + 1].chance)
                {
                    aa = rewardState[j];
                    rewardState[j] = rewardState[j + 1];
                    rewardState[j + 1] = aa;
                }

        float dice;
        float temp;
        for (int j = 0; j < RewardCount; j++)
        {
            dice = Random.Range(0, 101);
            temp = 0;
            for (int i = 0; i < rewardState.Length; i++)
            {
                temp += rewardState[i].chance;
                if (dice < temp)
                {
                    rewards.Add( RewardManager.Instance.AddReward(rewardState[i].type, rewardState[i].amount.Random));
                    break;
                }
            }
        }


        //add formula
        aa = new RewardStates();
        for (int i = 0; i < AdState.Length - 1; i++)
            for (int j = 0; j < AdState.Length - 1; j++)
                if (AdState[j].chance > AdState[j + 1].chance)
                {
                    aa = AdState[j];
                    AdState[j] = AdState[j + 1];
                    AdState[j + 1] = aa;
                }
        dice = Random.Range(0, 101);
        temp = 0;
        for (int i = 0; i < rewardState.Length; i++)
        {
            temp += rewardState[i].chance;
            if (dice < temp)
            {
                AdReward = RewardManager.Instance.AddReward(rewardState[i].type, rewardState[i].amount.Random);
                break;
            }
        }
        foreach (var item in rewards.ToArray())
        {
            print(item.type + " " + item.amount + " " + item.cardType + " " + item.characterId);
            RewardManager.Instance.AddReward(item);
        }

        //open chestViewer
        if (CampaignMenuManager.Instance != null)
            CampaignMenuManager.Instance.RenewPlayer();
    }

    [System.Serializable]
    public class RewardStates
    {

        public RewardType type;
        public int chance;
        public IntRange amount;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChestAnimation))]
public class Chest : MonoBehaviour {

    public int RewardCount;
    [Range(0,100)]
    public float chanceForADReward;
    public GameObject AdButton;
    public ChestAnimation Animation;
    public RewardStates[] rewardState;
    public RewardStates[] AdState;
    List<RewardInfo> rewards = new List<RewardInfo>();
    RewardInfo AdReward=null;
	// Use this for initialization
    public void MakeChest()
    {

        #region make reward

        #region Normal Rewards
        RewardStates aa = new RewardStates();
        rewards = new List<RewardInfo>();
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
                    rewards.Add( RewardManager.Instance.MakeReward(rewardState[i].type, rewardState[i].amount.Random));
                    break;
                }
            }
        }
        #endregion

        


        #endregion

        #region Add Normal Rewards

        foreach (var item in rewards.ToArray())
        {
            print(item.type + " " + item.amount + " " + item.potionType + " " + item.characterId);
            RewardManager.Instance.AddReward(item);
        }
        #endregion

        //open chestViewer
        if (CampaignMenuManager.Instance != null)
            CampaignMenuManager.Instance.RenewPlayer();

        Animation.Open(rewards, Random.Range(0, 101) <= chanceForADReward?true:false);

    }

    [System.Serializable]
    public class RewardStates
    {

        public RewardType type;
        public int chance;
        public IntRange amount;

    }

    void Reset()
    {
        if(GetComponent<ChestAnimation>()==null)
        Animation=gameObject.AddComponent<ChestAnimation>();
        else
            Animation = gameObject.GetComponent<ChestAnimation>();

    }
}

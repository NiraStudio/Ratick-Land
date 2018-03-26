using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement 
{
    public string FatTitle,EnTitle;
    public string id;
    public string tag;
    public Sprite Icon;
    public AchievementType achievementType;
    public bool resetable;
    public RewardType rewardType;
    public int rewardAmount;
    public int goalObject;
    public int currentObject;
    public string FaDes,EnDes;
    public bool achivmentDone;
    public bool RewardGained;

    public void Add(int amount)
    {
        if (achivmentDone)
            return;

        currentObject += amount;
        Check();
    }
    public void Check()
    {
        
        if (currentObject >= goalObject)
        {
            achivmentDone = true;
            AchievementManager.Instance.OpenAttention(this);
        }
    }
    public void GainReward()
    {
        if (RewardGained)
            return;
        RewardInfo r = RewardManager.Instance.MakeReward(rewardType, rewardAmount);
        RewardManager.Instance.AddReward(r);
        RewardGained = true;
    }
    public void Reset()
    {
        currentObject = 0;
    }
    public void Compelete()
    {
        achivmentDone = true;
        GainReward();
    }
}
public enum AchievementType
{
    killing, collecting, play, earnCoin, watchAdds,KillBoss,Specific
}

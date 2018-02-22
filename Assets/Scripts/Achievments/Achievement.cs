using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement 
{
    public string name;
    public string id;
    public Sprite Icon;
    public AchievementType achievementType;
    public bool achivmentDone;
    public bool resetable;
    public ChestReward reward=new ChestReward();
    public int goal;
    public int current;
    public string description;
    public bool RewardGained;

    public void Add(int amount)
    {
        if (achivmentDone)
            return;

        current += amount;
    }
    public void Check()
    {
        
        if (current >= goal)
        {
            achivmentDone = true;
            GainReward();
        }
    }
    public void GainReward()
    {
        if (RewardGained)
            return;
        reward.GainReward();
        RewardGained = true;
    }
    public void Reset()
    {
        current = 0;
    }
    public void Compelete()
    {
        achivmentDone = true;
        GainReward();
    }
}
public enum AchievementType
{
    killing, collecting, play, earnCoin, watchAdds,Specific
}

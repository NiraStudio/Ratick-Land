using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievment 
{
    public string name;
    public string id;
    public Sprite Icon;
    public AchievmentType achivType=0;
    public bool achivDone;
    public bool resetAble;
    public ChestReward reward=new ChestReward();
    public int goal;
    public int current;
    public string description;
    public void Check(int amount)
    {
        current += amount;
        if (current >= goal)
        {
            achivDone = true;
        }
    }
    public void GainReward()
    {
        reward.GainReward();
    }
    public void Reset()
    {
        current = 0;
    }
}
public enum AchievmentType
{
    killing, collecting, play, earnCoin, watchAdds
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AchievmentDataBase : ScriptableObject
{
    public List<Achievment> dB = new List<Achievment>();
    public void AddAchievment(Achievment achievment)
    {
        dB.Add(achievment);
    }
    public void DeleteAchievmentById(string id)
    {
        dB.Remove(GiveById(id));
    }
    public void DeleteAchievmentByName(string name)
    {
        dB.Remove(GiveByName(name));
    }
    public Achievment GiveByName(string name)
    {
        foreach(Achievment achievment in dB)
        {
            if (achievment.name == name)
            {
                return achievment;
            }
        }
        return null;
    }
    public Achievment GiveByIndex(int index)
    {
        return dB[index];
    }
    public Achievment GiveById(string id)
    {
        foreach (Achievment achievment in dB)
        {
            if (achievment.id == id)
            {
                return achievment;
            }
        }
        return null;
    }
    
}


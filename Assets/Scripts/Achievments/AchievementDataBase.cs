using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AchievementDataBase : ScriptableObject
{
    public List<Achievement> dataBase = new List<Achievement>();
    public void AddAchievment(Achievement achievment)
    {
        dataBase.Add(achievment);
        setDirty();
    }
    public void DeleteAchievmentById(string id)
    {
        dataBase.Remove(GiveById(id));
        setDirty();
        
    }
    
    public Achievement GiveByIndex(int index)
    {
        return dataBase[index];
    }
    public Achievement GiveById(string id)
    {
        foreach (Achievement achievment in dataBase)
        {
            if (achievment.id == id)
            {
                return achievment;
            }
        }
        return null;
    }
    void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}


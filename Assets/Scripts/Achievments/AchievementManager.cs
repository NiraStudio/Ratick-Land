using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    #region singlton
    public static AchievementManager Instance;
    void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(this);
    }
    #endregion

    public AchievementDataBase achievementDataBase;


    void Start()
    {

    }

    void Update()
    {

    }


    public void Restart()
    {
        foreach(Achievement achiv in achievementDataBase.dataBase)
        {
            if (achiv.resetable)
            {
                achiv.Reset();
            }
        }    
    }
   public void Compelete(AchievementType type)
    {
        foreach (var item in achievementDataBase.dataBase)
        {
            if(item.achievementType ==type)
            item.Compelete();
        }
    }
    public void Compelete(string ID)
    {
        foreach (var item in achievementDataBase.dataBase)
        {
            if (item.id == ID)
                item.Compelete();
        }
    }

    public void Add(AchievementType type,int amount)
    {
        foreach (var item in achievementDataBase.dataBase)
        {
            if (item.achievementType  == type)
                item.Add(amount);
        }
    }

    public void Add(string ID,int amount)
    {
        foreach (var item in achievementDataBase.dataBase)
        {
            if (item.id == ID)
                item.Add(amount);
        }
    }

}

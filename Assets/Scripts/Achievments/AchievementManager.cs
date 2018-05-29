using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alpha.Localization;

public class AchievementManager : MonoBehaviour {

    #region singlton
    public static AchievementManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    #endregion

    public AchievementDataBase achievementDataBase;
    public GameObject AchivementUI;
    public GameObject AchivementContent,AchivementPanel;
    public Animator AchivementAttention;

    void Start()
    {
        MakePanel();
        AchivementPanel.SetActive(false); 
    }


    public void MakePanel()
    {

        for (int i = 0; i < achievementDataBase.dataBase.Count; i++)
        {
            Instantiate(AchivementUI, AchivementContent.transform).SendMessage("Repaint", achievementDataBase.GiveByIndex(i));
        }
    }
    public void Repaint()
    {
        for (int i = 0; i < achievementDataBase.dataBase.Count; i++)
        {
            AchivementContent.transform.GetChild(i).SendMessage("RePaint");
        }
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

    public void Add(string Tag,int amount)
    {
        foreach (var item in achievementDataBase.dataBase)
        {
            if (item.tag == Tag)
                item.Add(amount);
        }
    }
    public void OpenAttention(Achievement data)
    {
        AchivementAttention.SetTrigger("Open");
        AchivementAttention.transform.GetChild(0).GetComponent<LocalizedDynamicText>().Text(data.FatTitle, data.EnTitle);
        Repaint();
    }
    public void PanelState(bool State)
    {
        AchivementPanel.SetActive(State);
        Repaint();
    }

}

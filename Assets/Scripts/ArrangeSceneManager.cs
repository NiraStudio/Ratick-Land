using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeSceneManager : MainBehavior {
    public static ArrangeSceneManager Instance;
    public CharacterDataBase characterDatabase;
    public GameObject choosePanel,ChoosePanelParent,ChooseButton,DeleteButton;
    public ArrangeIcon[] Heros;
    public ArrangeIcon Main, Support, Minion;

    public CharacterData[] data;

    List<CharacterData> temp = new List<CharacterData>();
    SlotContainer sc = new SlotContainer();
    GameManager GM;
    ArrangeIcon currentIcon;
    void Awake()
    {
        Instance = this;
        GM = GameManager.instance;
    }
	// Use this for initialization
	void Start () {
        RepaintArranges();
	}

    void RepaintArranges()
    {
        if (GM.SlotData == null)
            return;

        for (int i = 0; i < GM.SlotData.Heros.Count; i++)
        {
            if (GM.SlotData.Heros[i] >= 0)
                Heros[i].Repaint(characterDatabase.GiveByID(GM.SlotData.Heros[i]));
        }
        if (GM.SlotData.mainId >= 0)
            Main.Repaint(characterDatabase.GiveByID(GM.SlotData.mainId));

        if (GM.SlotData.minionId >= 0)
            Minion.Repaint(characterDatabase.GiveByID(GM.SlotData.minionId));

        if (GM.SlotData.supportId >= 0)
            Support.Repaint(characterDatabase.GiveByID(GM.SlotData.supportId));

    }
    public void OpenChoosePanel(ArrangeIcon btn)
    {
        currentIcon = btn;
        choosePanel.SetActive(true);

        if (currentIcon.ID >= 0)
            DeleteButton.gameObject.SetActive(true);
        else
            DeleteButton.gameObject.SetActive(false);

        MakeChoosePanel(btn.type);

        print("Open");
    }
    public void CloseChoosePanel(CharacterData data)
    {
        currentIcon.Repaint(data);
        choosePanel.SetActive(false);
    }
    public void Clean()
    {
        currentIcon.Clean();
        choosePanel.SetActive(false);
    }
    public void CloseChoosePanel()
    {
        choosePanel.SetActive(false);
    }
    public void MakeChoosePanel(CharacterData.Type t)
    {
        for (int i = 1; i < ChoosePanelParent.transform.childCount; i++)
        {
            Destroy(ChoosePanelParent.transform.GetChild(i).gameObject);
        }
        temp = new List<CharacterData>();
        foreach (var item in GM.mainData.charactersData)
        {
            CharacterData a = characterDatabase.GiveByID(item.Key);
            if (a.type == t)
                temp.Add(a);
        }
        foreach (var item in temp.ToArray())
        {
            if(CheckForSelect(item.id)==false)
            Instantiate(ChooseButton, ChoosePanelParent.transform).SendMessage("Repaint", item);
        }
        
    }
    public void GoToPlayScene()
    {
        if (!CheckForRequirements())
            return;


        List<int> hero = new List<int>();
        foreach (var item in Heros)
        {
            if (item.ID > 0)
                hero.Add(item.ID);
            
        }

        sc.ChangeHeros(hero);
        
        sc.ChangeMain(Main.ID>0?Main.ID:-1, Main.Level);
        sc.ChangeMinion(Minion.ID > 0 ? Minion.ID : -1, Minion.Level);
        sc.ChangeSupport(Support.ID > 0 ? Support.ID : -1, Support.Level);

        GM.SlotData = sc;
        GoToScene("PlayScene");
        
    }
    bool CheckForSelect(int ID)
    {
        bool answer=false;
        foreach (var item in Heros)
        {
            if(ID==item.ID)
            {
                answer = true;
                return answer;
            }
        }
        if(ID==Minion.ID)
        {
            answer = true;
            return answer;
        }

        if (ID == Support.ID)
        {
            answer = true;
            return answer;
        }

        if (ID == Main.ID)
        {
            answer = true;
            return answer;
        }
        return answer;
    }
    bool CheckForRequirements()
    {
        if (Main.ID <= 0)
        {
            print("You Didnt Choose Any Leader");
            return false;
        }
        /*if(Minion.ID<=0)
        {
            print("You Didnt Choose Any Minion");
            return false;
        }
        */
        return true;
    }
    
}

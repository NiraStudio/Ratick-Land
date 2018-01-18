using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeSceneManager : MainBehavior {
    public static ArrangeSceneManager Instance;
    public CharacterDataBase characterDatabase;
    public GameObject choosePanel,ChoosePanelParent,ChooseButton;
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
     //   MakeChoosePanel();
	}
	
	
    public void OpenChoosePanel(ArrangeIcon btn)
    {
        currentIcon = btn;
        choosePanel.SetActive(true);
        MakeChoosePanel(btn.type);
        print("Open");
    }
    public void CloseChoosePanel(CharacterData data)
    {
        currentIcon.Repaint(data);
        choosePanel.SetActive(false);
    }
    public void CloseChoosePanel()
    {
        choosePanel.SetActive(false);
    }
    public void MakeChoosePanel(CharacterData.Type t)
    {
        for (int i = 0; i < ChoosePanelParent.transform.childCount; i++)
        {
            Destroy(ChoosePanelParent.transform.GetChild(i).gameObject);
        }
        temp = new List<CharacterData>();

        temp = characterDatabase.GiveByType(t);
        foreach (var item in temp.ToArray())
        {
            if(CheckForSelect(item.id)==false)
            Instantiate(ChooseButton, ChoosePanelParent.transform).SendMessage("Repaint", item);
        }
        
    }
    public void GoToPlayScene()
    {
        Dictionary<int, int> hero = new Dictionary<int, int>();
        foreach (var item in Heros)
        {
            if (item.ID > 0)
                hero.Add(item.ID, item.Level);
            
        }

        sc.ChangeHeros(hero);
        
        sc.ChangeMain(Main.ID>0?Main.ID:-1, Main.Level);
        sc.ChangeMinion(Minion.ID > 0 ? Minion.ID : -1, Minion.Level);
        sc.ChangeSupport(Support.ID > 0 ? Support.ID : -1, Support.Level);

        GM.SlotData=sc;

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
    
}

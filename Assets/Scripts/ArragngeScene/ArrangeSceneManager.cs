using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeSceneManager : MainBehavior {
    public static ArrangeSceneManager Instance;
    public CharacterDataBase characterDatabase;
    public GameObject choosePanel,ChoosePanelParent,ChooseButton,DeleteButton,CardPanel;
    public ArrangeIcon[] Heros;
    public ArrangeIcon Main, Support, Minion;
    public Text CardDoubleAttackText, CardDoubleCoinText;
    public Image CardBtnImage;
    public CharacterData[] data;

    List<CharacterData> temp = new List<CharacterData>();
    SlotContainer sc = new SlotContainer();
    GameManager GM;
    ArrangeIcon currentIcon;

    int CardDoubleAttack, CardDoubleCoin;
    void Awake()
    {
        Instance = this;
        GM = GameManager.instance;
    }
	// Use this for initialization
    void Start()
    {
        if (GM.SlotData != null)
            RepaintArranges(GM.SlotData);

        OpenScreen();
    }


    //character Panel

    void RepaintArranges(SlotContainer SC)
    {
        

        for (int i = 0; i < SC.Heros.Count; i++)
        {
            if (SC.Heros[i] > 0)
                Heros[i].Repaint(characterDatabase.GiveByID(SC.Heros[i]));
        }
        if (SC.mainId > 0)
        {
            Main.Repaint(characterDatabase.GiveByID(SC.mainId));
        }

        if (SC.minionId > 0)
            Minion.Repaint(characterDatabase.GiveByID(SC.minionId));

        if (SC.supportId > 0)
            Support.Repaint(characterDatabase.GiveByID(SC.supportId));


    }
    public void OpenCharacterChoosePanel(ArrangeIcon btn)
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
    public void CloseCharacterChoosePanel(CharacterData data)
    {
        currentIcon.Repaint(data);
        choosePanel.SetActive(false);
    }
    public void CloseCharacterChoosePanel()
    {
        choosePanel.SetActive(false);
    }


    //card panel

    public void OpenCardPanel()
    {
        CardPanel.SetActive(true);
        CardDoubleAttack = CardDoubleCoin = 0;


        foreach (var item in GM.mainData.cardHolder.cards.ToArray())
        {
            switch (item.cardType)
            {
                case Card.Type.empty:
                    break;
                case Card.Type.DoubleCoin:
                    CardDoubleCoin++;
                    break;
                case Card.Type.DoubleATK:
                    CardDoubleAttack++;
                    break;

            }
        }

        CardDoubleAttackText.text = "X" + CardDoubleAttack;
        CardDoubleCoinText.text = "X" + CardDoubleCoin;
    }
    public void CloseCardPanel(Card card)
    {
        
        switch (card.cardType)
        {
            case Card.Type.empty:
                break;
            case Card.Type.DoubleCoin:
                if (CardDoubleCoin > 0)
                {

                CardBtnImage.color = Color.yellow;
                    sc.card = card;
                }
                break;
            case Card.Type.DoubleATK:
                if (CardDoubleAttack > 0)
                {

                    CardBtnImage.color = Color.red;

                    sc.card = card;
                }

                break;
        }
        CardPanel.SetActive(false);

    }
    public void CloseCardPanel()
    {

        sc.card = null;
        CardBtnImage.sprite = null;
        CardPanel.SetActive(false);

    }


    public void Clean()
    {
        currentIcon.Clean();
        choosePanel.SetActive(false);
    }
    public void MakeChoosePanel(CharacterData.Type t)
    {
        for (int i = 1; i < ChoosePanelParent.transform.childCount; i++)
        {
            Destroy(ChoosePanelParent.transform.GetChild(i).gameObject);
        }
        temp = new List<CharacterData>();
        foreach (var item in GM.mainData.characterInfos.ToArray())
        {
            CharacterData a = characterDatabase.GiveByID(item.Id);
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
        SaveSlot();
        GoToScene("PlayScene");
        
    }
    public void SaveSlot()
    {
        List<int> hero = new List<int>();
        foreach (var item in Heros)
        {
            if (item.ID > 0)
                hero.Add(item.ID);

        }

        sc.ChangeHeros(hero);

        sc.ChangeMain(Main.ID > 0 ? Main.ID : -1, Main.Level);
        sc.ChangeMinion(Minion.ID > 0 ? Minion.ID : -1, Minion.Level);
        sc.ChangeSupport(Support.ID > 0 ? Support.ID : -1, Support.Level);

        GM.SlotData = sc;
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

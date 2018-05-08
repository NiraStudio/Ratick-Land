using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class ArrangeSceneManager : MainBehavior
{
    public static ArrangeSceneManager Instance;
    public CharacterDataBase characterDatabase;
    public GameObject choosePanel, ChoosePanelParent, ChooseButton, DeleteButton, CardPanel;
    public ArrangeIcon[] Heros;
    public ArrangeIcon Main, Support, Minion;
    [Header("Card Data")]
    public LocalizedDynamicText PotionDoubleAttackText;
    public LocalizedDynamicText PotionDoubleCoinText;
    public Image PotionBtnImage,DoubleCoinPotionImg,DoubleDmgPotionImg,ChoosePanelImage;
    public CharacterData[] data;
    public LocalizedKeyText ChoosePanelText;
    List<CharacterData> temp = new List<CharacterData>();
    SlotContainer sc = new SlotContainer();
    GameManager GM;
    ArrangeIcon currentIcon;

    int PotionDoubleAttack, PotionDoubleCoin;
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
        if (PlayerPrefs.GetInt("Tutorial") == 1)
            TutorialManager.Instance.OpenStep("Tut_3");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GoToScene("MainMenu");
    }
    //character Panel

    void RepaintArranges(SlotContainer SC)
    {


        for (int i = 0; i < SC.Heros.Count; i++)
        {
            if (SC.Heros[i] > 0)
                if(GM.DoesPlayerHasThisCharacter(SC.Heros[i]))
                Heros[i].Repaint(characterDatabase.GiveByID(SC.Heros[i]));
        }
        if (SC.mainId > 0)
        {
            if (GM.DoesPlayerHasThisCharacter(SC.mainId))
                Main.Repaint(characterDatabase.GiveByID(SC.mainId));
        }

        if (SC.minionId > 0)
            if (GM.DoesPlayerHasThisCharacter(SC.minionId))
                Minion.Repaint(characterDatabase.GiveByID(SC.minionId));

        if (SC.supportId > 0)
            if (GM.DoesPlayerHasThisCharacter(SC.supportId))
                Support.Repaint(characterDatabase.GiveByID(SC.supportId));


    }

    public void MinionChange(int id)
    {
        Minion.Repaint(characterDatabase.GiveByID(id));

    }
    public void SuppChange(int id)
    {
        Support.Repaint(characterDatabase.GiveByID(id));

    }
    public void HeroChange(int id, int heroIconId)
    {
        Heros[heroIconId].Repaint(characterDatabase.GiveByID(id));

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
        PotionDoubleAttack = PotionDoubleCoin = 0;


        foreach (var item in GM.mainData.porionHolder.cards.ToArray())
        {
            switch (item.cardType)
            {
                case Potion.Type.empty:
                    break;
                case Potion.Type.DoubleCoin:
                    PotionDoubleCoin++;
                    break;
                case Potion.Type.DoubleATK:
                    PotionDoubleAttack++;
                    break;

            }
        }


        PotionDoubleAttackText.Number = "X" + PotionDoubleAttack;
        PotionDoubleCoinText.Number = "X" + PotionDoubleCoin;
    }
    public void CloseCardPanel(Potion potion)
    {

        switch (potion.cardType)
        {
            case Potion.Type.empty:
                break;
            case Potion.Type.DoubleCoin:
                if (PotionDoubleCoin > 0)
                {

                    PotionBtnImage.sprite =DoubleCoinPotionImg.sprite;
                    sc.porion = potion;
                }
                break;
            case Potion.Type.DoubleATK:
                if (PotionDoubleAttack > 0)
                {

                    PotionBtnImage.sprite = DoubleDmgPotionImg.sprite;

                    sc.porion = potion;
                }

                break;
        }
        CardPanel.SetActive(false);

    }
    public void CloseCardPanel()
    {

        sc.porion = null;
        PotionBtnImage.sprite = null;
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
        if (temp.Count > 0)
        {
            foreach (var item in temp.ToArray())
            {
                if (CheckForSelect(item.id) == false)
                {
                    GameObject g = Instantiate(ChooseButton, ChoosePanelParent.transform);
                    g.SendMessage("Repaint", item);
                    g.SendMessage("Enable");

                }
            }
        }
        else
        {

        }

    }
    public void GoToPlayScene()
    {
        if (!CheckForRequirements())
            return;
        SaveSlot();
        GoToScene("ArenaScene");
        GM.bgm.stopSound();
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
        bool answer = false;
        foreach (var item in Heros)
        {
            if (ID == item.ID)
            {
                answer = true;
                return answer;
            }
        }
        if (ID == Minion.ID)
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

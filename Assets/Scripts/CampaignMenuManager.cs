using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CampaignMenuManager : MainBehavior {
    #region Singleton
    public static CampaignMenuManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion



    public List<CharacterData> PlayerCharacters = new List<CharacterData>();
    public ScrollRectSnap CharacterScroll;//,SkinScroll;
    public GameObject characterInstansiatePos, CampaignCard;//,CampaignSkinCard;
    public Animator doorAnimation;
    public Button UpgradeButton;
    public CharacterCampaignCard[] cardHolders;
    GameManager GM;

    [SerializeField]
    CampaignDetailPanelBehaivior detailHolder;
    [SerializeField]
    Skin ss;
    List<string> SD = new List<string>();
    List<SkinData> skinData = new List<SkinData>();
    CharacterData CurrentCharacter;
	// Use this for initialization
	IEnumerator Start () {
        GM = GameManager.instance;
        RenewPlayer();

        yield return new WaitUntil(() => characterInstansiatePos.transform.childCount > 0);
        LoadingScreenManager.Instance.Open();
       // UpgradeButton.onClick.AddListener(UpgradeCharacter);
	}

    // Update is called once per frame
    /*void Update()
    {
        if (CurrentCharacter != null)
        {

            if (GM.CharacterCard(CurrentCharacter.id) < GM.CharacterCardUpgradeCost(CurrentCharacter.id) || GM.coinAmount < GM.CharacterUpgradeCost(CurrentCharacter))
            {
                string a = "";
                if (GM.CharacterCard(CurrentCharacter.id) < GM.CharacterCardUpgradeCost(CurrentCharacter.id))
                {
                    a = GM.CharacterCard(CurrentCharacter.id).ToString() + "/" + GM.CharacterCardUpgradeCost(CurrentCharacter.id).ToString();
                    UpgradeButton.transform.GetChild(1).gameObject.SetActive(false);

                }

                else if (GM.coinAmount < GM.CharacterUpgradeCost(CurrentCharacter))
                {
                    a = GM.CharacterUpgradeCost(CurrentCharacter.id).ToString();
                    UpgradeButton.transform.GetChild(1).gameObject.SetActive(true);

                }

                UpgradeButton.transform.GetChild(2).GetComponent<LocalizedDynamicText>().text = a;
                UpgradeButton.interactable = false;
            }
            else
            {

                UpgradeButton.transform.GetChild(2).GetComponent<LocalizedDynamicText>().text = GM.CharacterUpgradeCost(CurrentCharacter.id).ToString();
                UpgradeButton.transform.GetChild(1).gameObject.SetActive(true);

                UpgradeButton.interactable = true;
            }
        }
        else
        {
            UpgradeButton.gameObject.SetActive(false);
        }
    }*/

    public void RenewPlayer()
    {
        int aa =0;
        for (int i = 0; i < CharacterScroll.Content.transform.childCount; i++)
        {
            Destroy(CharacterScroll.Content.transform.GetChild(i).gameObject);
        }
        PlayerCharacters = GM.PlayerCharacters;
        cardHolders = new CharacterCampaignCard[GM.characterDB.Count];
        for (int i = 0; i < GM.characterDB.Count; i++)
        {
            cardHolders[i]= Instantiate(CampaignCard, CharacterScroll.Content.transform).GetComponent<CharacterCampaignCard>();
            if (GM.characterDB.DataBase[i].type == CharacterData.Type.Leader)
            {
                ChangeCharacter(GM.characterDB.DataBase[i]);
                aa = i+1;
            }

            cardHolders[i].Repaint(GM.characterDB.DataBase[i]);
        }
        CharacterScroll.StartArrange(aa);
    }

    public void ChangeCharacter(CharacterData data)
    {
        CurrentCharacter = data;
        doorAnimation.SetTrigger("Close");
        print("Changed 1");

    }

    /*public void RenewSkins()
    {
        skinData = new List<SkinData>();
        SD = new List<string>();

        skinData = GM.skinDB.CharacterBoughtedSkins(CurrentCharacter.id);
        if (skinData == null)
            return;
        SD = GM.CharacterBoughtedSkins(CurrentCharacter.id);
        for (int i = 0; i < SkinScroll.Content.transform.childCount; i++)
        {
            Destroy(SkinScroll.Content.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < skinData.Count; i++)
        {
            CharacterSkinCampaignCard g= Instantiate(CampaignSkinCard, CharacterScroll.Content.transform).GetComponent<CharacterSkinCampaignCard>();
            if (SD.Contains(skinData[i].SkinName))
            {
                if (skinData[i].SkinName == GM.CurrentCharacterSkin(CurrentCharacter.id))
                    g.Repaint(skinData[i], true, true);
                else
                    g.Repaint(skinData[i], true, false);

            }
            else
            {
                g.Repaint(skinData[i], false, false);

            }
        }
    }*/

    public void ChangeModelSkin(string s)
    {
        characterInstansiatePos.transform.GetChild(0).GetComponent<SkinManager>().LoadSkinByString(s);
    }

    public void UpgradeCharacter()
    {
        GM.ChangeCoin(-GM.CharacterUpgradeCost(CurrentCharacter));
        GM.AddCharacterCard(CurrentCharacter.id, -GM.CharacterCardUpgradeCost(CurrentCharacter.id));
        GM.IncreaseCharacterLevel(CurrentCharacter.id, 1);
        GameAnalyticsManager.SendCustomEvent("Character Upgrade:"+CurrentCharacter.characterName);
        SFX.Instance.PlaySound("LevelUp");
        detailHolder.RePaint(CurrentCharacter);
        for (int i = 0; i < CharacterScroll.Content.transform.childCount; i++)
        {
            CharacterScroll.Content.transform.GetChild(i).SendMessage("RepaintCheck");
        }
    }
    public void ChangeCurrentCharacter()
    {
       // UpgradeButton.gameObject.SetActive(true);

        if (characterInstansiatePos.transform.childCount > 0)
            for (int i = 0; i < characterInstansiatePos.transform.childCount; i++)
                Destroy(characterInstansiatePos.transform.GetChild(i).gameObject);

        bool a=false;
        foreach (var item in PlayerCharacters.ToArray())
        {
            if (item.id == CurrentCharacter.id)
            {
                a = true;
                break;
            }
        }
        GameObject g = Instantiate(CurrentCharacter.prefab, characterInstansiatePos.transform.position, Quaternion.identity);
        Vector3 aa = g.transform.localScale;
        if (!a) g.SendMessage("SetBlack");
        g.transform.SetParent(characterInstansiatePos.transform);
        g.GetComponent<Character>().enabled = false;
        g.transform.localScale = aa;
        doorAnimation.SetTrigger("Open");
        print("Changed");
       // detailHolder.RePaint(data);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CampaignMenuManager : MonoBehaviour {
    #region Singleton
    public static CampaignMenuManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion



    public List<CharacterData> PlayerCharacter = new List<CharacterData>();
    public ScrollRectSnap CharacterScroll,SkinScroll;
    public GameObject characterInstansiatePos,CampaignCard,CampaignSkinCard;
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
	void Start () {
        GM = GameManager.instance;
        RenewPlayer();
        UpgradeButton.onClick.AddListener(UpgradeCharacter);
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentCharacter!=null)
        {
            UpgradeButton.transform.GetChild(0).GetComponent<LocalizedDynamicText>().Number =GM.CharacterUpgradeCost(CurrentCharacter).ToString();
            if (GM.CharacterCard(CurrentCharacter.id) < GM.CharacterCardUpgradeCost(CurrentCharacter.id) || GM.coinAmount< GM.CharacterUpgradeCost(CurrentCharacter))
                UpgradeButton.interactable = false;
            else
                UpgradeButton.interactable = true;
        }
        else
        {
            UpgradeButton.gameObject.SetActive(false);
        }
	}

    public void RenewPlayer()
    {
        int aa =0;
        for (int i = 0; i < CharacterScroll.Content.transform.childCount; i++)
        {
            Destroy(CharacterScroll.Content.transform.GetChild(i).gameObject);
        }
        PlayerCharacter = GM.PlayerCharacters;
        cardHolders = new CharacterCampaignCard[PlayerCharacter.Count];
        for (int i = 0; i < PlayerCharacter.Count; i++)
        {
            cardHolders[i]= Instantiate(CampaignCard, CharacterScroll.Content.transform).GetComponent<CharacterCampaignCard>();
            if (PlayerCharacter[i].type == CharacterData.Type.Leader)
            {
                ChangeCurrentCharacter(PlayerCharacter[i]);
                aa = i+1;


            }

            cardHolders[i].Repaint(PlayerCharacter[i]);
        }
        CharacterScroll.StartArrange(aa);
        RenewSkins();
    }
    public void RenewSkins()
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
    }

    public void ChangeModelSkin(string s)
    {
        characterInstansiatePos.transform.GetChild(0).GetComponent<SkinManager>().LoadSkinByString(s);
    }

    public void UpgradeCharacter()
    {
        GM.ChangeCoin(-GM.CharacterUpgradeCost(CurrentCharacter));
        GM.AddCharacterCard(CurrentCharacter.id, -GM.CharacterCardUpgradeCost(CurrentCharacter.id));
        GM.IncreaseCharacterLevel(CurrentCharacter.id, 1);
        GameAnalyticsManager.SendCustomEvent(CurrentCharacter.characterName);
    }
    public void ChangeCurrentCharacter(CharacterData data)
    {
        UpgradeButton.gameObject.SetActive(true);

        if (characterInstansiatePos.transform.childCount > 0)
            for (int i = 0; i < characterInstansiatePos.transform.childCount; i++)
                Destroy(characterInstansiatePos.transform.GetChild(i).gameObject);


        CurrentCharacter = data;
        GameObject g = Instantiate(data.prefab, characterInstansiatePos.transform.position, Quaternion.identity);
        Vector3 aa = g.transform.localScale; 
        g.transform.SetParent(characterInstansiatePos.transform);
        g.GetComponent<Character>().enabled = false;
        g.transform.localScale = aa;
        detailHolder.RePaint(data,ss);

    }
}

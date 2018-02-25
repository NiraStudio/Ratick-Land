using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignMenuManager : MonoBehaviour {
    #region Singleton
    public static CampaignMenuManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion



    public List<CharacterData> PlayerCharacter = new List<CharacterData>();
    public ScrollRectSnap CharacterScroll;
    public GameObject characterInstansiatePos,CampaignCard;
    public Button UpgradeButton;
    public CharacterCampaignCard[] cardHolders;
    GameManager GM;
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
            UpgradeButton.transform.GetChild(0).GetComponent<Text>().text = GM.UpgradeCost(CurrentCharacter).ToString();
            if (GM.CharacterCard(CurrentCharacter.id) < GM.CharacterCardUpgradeCost(CurrentCharacter.id) || GM.coinAmount< GM.UpgradeCost(CurrentCharacter))
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

    }

    public void UpgradeCharacter()
    {
        GM.ChangeCoin(-GM.UpgradeCost(CurrentCharacter));
        GM.AddCharacterCard(CurrentCharacter.id, -GM.CharacterCardUpgradeCost(CurrentCharacter.id));
        GM.IncreaseLevel(CurrentCharacter.id, 1);
    }
    public void ChangeCurrentCharacter(CharacterData data)
    {
        UpgradeButton.gameObject.SetActive(true);

        if (characterInstansiatePos.transform.childCount > 0)
            for (int i = 0; i < characterInstansiatePos.transform.childCount; i++)
                Destroy(characterInstansiatePos.transform.GetChild(i).gameObject);


        CurrentCharacter = data;
        GameObject g = Instantiate(data.prefab, characterInstansiatePos.transform.position, Quaternion.identity);
        g.transform.SetParent(characterInstansiatePos.transform);
        g.GetComponent<Character>().enabled = false;
        g.transform.localScale = Vector3.one;

    }
}

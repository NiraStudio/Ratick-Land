using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CharacterCampaignCard : MonoBehaviour {
    public Image CharacterImg;
    public GameObject Alart;
    public CharacterData data;
    public LocalizedKeyText CharacterName;
    GameManager GM;
    void Start()
    {
        GM = GameManager.instance;
    }
    
	// Use this for initialization
	public void Repaint(CharacterData data)
    {
        Alart.SetActive(false);
        GM = GameManager.instance;
        this.data = data;
        CharacterImg.sprite = data.icon;
        CharacterName.Key = data.characterName;
        if (GM.DoesPlayerHasThisCharacter(data.id))
        {
            if (GM.CharacterCard(data.id) >= GM.CharacterCardUpgradeCost(data.id) && GM.coinAmount >= GM.CharacterUpgradeCost(data.id))
                Alart.SetActive(true);
            else
                Alart.SetActive(false);
        }
    }
    public void RepaintCheck()
    {
        CharacterImg.sprite = data.icon;
        CharacterName.Key = data.characterName;

        if (GM.CharacterCard(data.id) >= GM.CharacterCardUpgradeCost(data.id) && GM.coinAmount >= GM.CharacterUpgradeCost(data.id))
            Alart.SetActive(true);
        else
            Alart.SetActive(false);
    }

}

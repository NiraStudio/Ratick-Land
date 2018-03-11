using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCampaignCard : MonoBehaviour {
    public Image CharacterImg;
    public GameObject Alart;
    public CharacterData data;

    GameManager GM;
    void Start()
    {
        GM = GameManager.instance;
    }
    
	// Use this for initialization
	public void Repaint(CharacterData data)
    {
        GM = GameManager.instance;
        this.data = data;
        CharacterImg.sprite = data.icon;
        if (GM.CharacterCard(data.id) >= GM.CharacterCardUpgradeCost(data.id)&&GM.coinAmount>=GM.CharacterUpgradeCost(data.id))
            Alart.SetActive(true);
        else
            Alart.SetActive(false);
    }
    public void Repaint()
    {
        CharacterImg.sprite = data.icon;
        if (GM.CharacterCard(data.id) >= GM.CharacterCardUpgradeCost(data.id) && GM.coinAmount >= GM.CharacterUpgradeCost(data.id))
            Alart.SetActive(true);
        else
            Alart.SetActive(false);
    }

}

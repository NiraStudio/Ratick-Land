using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelBtn : MonoBehaviour {
    public Text characterNameText,levelText,upgradeBtnText;
    public Image icon;

    public CharacterData data;

    GameManager GM;
    int level;
    int cost;
    void FixedUpdate()
    {
        if (GM.coinAmount < cost)
            upgradeBtnText.transform.parent.GetComponent<Button>().interactable = false;
        else
            upgradeBtnText.transform.parent.GetComponent<Button>().interactable = true;
    }
    public void Repaint(CharacterData data)
    {
        if (GM == null)
            GM = GameManager.instance;

        this.data = data;
        characterNameText.text = data.characterName;
        levelText.text = GM.CharacterLevel(data.id).ToString();
        cost = GM.UpgradeCost(data);
        upgradeBtnText.text = cost.ToString();

        

        icon.sprite = data.icon;
    }
    public void Upgrade()
    {
        GM.ChangeCoin(-cost);
        GM.IncreaseLevel(data.id, 1);
        Repaint(data);
    }
}

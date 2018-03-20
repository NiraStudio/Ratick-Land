using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CharacterSkinCampaignCard : MonoBehaviour {
    public Image Icon;
    public Button ChooseBtn, GoToShopBtn;
    public LocalizedKeyText ChooseTxt, GoToShopTxt;

    void Start()
    {
        ChooseBtn.onClick.AddListener(ChooseMethod);
        GoToShopBtn.onClick.AddListener(GoToShopMethod);
        
    }

    SkinData data;
	// Use this for initialization
	public void Repaint(SkinData data,bool HasSkin,bool Choosed)
    {
        ChooseBtn.gameObject.SetActive(false);
        GoToShopBtn.gameObject.SetActive(false);

        data = this.data;

        Icon.sprite = data.Icon;

        if (HasSkin)
        {
            ChooseBtn.gameObject.SetActive(true);
            if (Choosed)
            {
                ChooseTxt.Key = "Choosed";
                ChooseBtn.interactable = false;
            }
            else
            {
                ChooseBtn.interactable = true; ;

                ChooseTxt.Key = "Choose";

            }

        }
        else
        {

        }

    }

    public void GoToShopMethod()
    {

    }
    public void ChooseMethod()
    {

    }
}

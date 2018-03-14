using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CharacterSkinCampaignCard : MonoBehaviour {
    public Image Icon;
    public Button ChooseBtn, GoToShopBtn;
    public LocalizedText ChooseTxt, GoToShopTxt;

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
                ChooseTxt.ChangeKey = "Choosed";
                ChooseBtn.interactable = false;
            }
            else
            {
                ChooseBtn.interactable = true; ;

                ChooseTxt.ChangeKey = "Choose";

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

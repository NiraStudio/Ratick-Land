using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestCard : MonoBehaviour {
    public Image CardImage;
    public Text CardText;
    public Slider CardSlider;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

    }
    public void Repaint(ChestReward Reward)
    {
        CardImage.sprite = Reward.icon;
        CardText.text = "X " + Reward.amount;


        if (Reward.type == ChestReward.Type.UpgradeCard)
        {
            CardSlider.gameObject.SetActive(true);
            //get Max Card Need From GM
            //Change Max Value
            //Gem Current Amount + Reward Amount
            //Change Value

        }
        else
            CardSlider.gameObject.SetActive(false);
    }

}

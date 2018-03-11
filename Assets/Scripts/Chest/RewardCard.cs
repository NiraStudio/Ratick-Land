using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCard : MonoBehaviour {
    public Image CardImage;
    public Text CardName, CardAmount;
    public Slider CardSlider;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

    }
    public void Repaint(RewardInfo Reward)
    {
        CardImage.sprite = Reward.Icon;
        CardAmount.text = "X " + Reward.amount;
        CardName.text = Reward.RewardName;
        
       
    }

}

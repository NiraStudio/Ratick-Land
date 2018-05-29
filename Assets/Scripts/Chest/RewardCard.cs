using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class RewardCard : MonoBehaviour {
    public Image CardImage;
    public LocalizedDynamicText CardAmount;
    public LocalizedKeyText CardName;

    Image img;
    void Start()
    {
    }
    // Use this for initialization

    // Update is called once per frame
   
    public void Repaint(RewardInfo Reward)
    {
        img = GetComponent<Image>();
        Color c = img.color;
        if (Reward.type == RewardType.UpgradeCard || Reward.type == RewardType.NewCharacter)
            img.color = c;
        else
        {
            c.a = 1;
            img.color = c;
        }
        CardImage.sprite = Reward.Icon;
        CardAmount.text = "X " + Reward.amount;
        CardName.Key = Reward.RewardName;
        
       
    }

}

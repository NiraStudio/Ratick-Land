using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class AchievmentUI : MonoBehaviour {
    public LocalizedDynamicText AchiveName,RewardAmount,SliderText,AchiveDes;
    public Image Icon;
    public Slider slider;
    public Button Btn;
    Achievement data;
	// Use this for initialization

    public void Repaint(Achievement d)
    {
        data = d;
        AchiveName.Text(data.FatTitle, data.EnTitle);
        AchiveDes.Text(data.FaDes, data.EnDes);
        SliderText.text = data.currentObject + "/" + data.goalObject;
        slider.maxValue = data.goalObject;
        slider.value = data.currentObject;
        if (data.achievementType == AchievementType.Specific)
            slider.gameObject.SetActive(false);
        RewardAmount.text = data.rewardAmount.ToString();
        RewardInfo a = RewardManager.Instance.MakeReward(data.rewardType, data.rewardAmount);
        Icon.sprite = a.Icon;
        if (data.RewardGained)
        {
            Btn.interactable = false;
            Btn.GetComponent<Image>().color = Color.green;
            return;

        }
        if (data.achivmentDone)
        {
            Btn.GetComponent<Animator>().SetBool("Open",true);
        }

    }
    public void RePaint()
    {
        AchiveName.Text(data.FatTitle, data.EnTitle);
        AchiveDes.Text(data.FaDes, data.EnDes);
        SliderText.text = data.currentObject + "/" + data.goalObject;
        slider.maxValue = data.goalObject;
        slider.value = data.currentObject;
        if (data.achievementType == AchievementType.Specific)
            slider.gameObject.SetActive(false);
        RewardAmount.text = data.rewardAmount.ToString();
        RewardInfo a = RewardManager.Instance.MakeReward(data.rewardType, data.rewardAmount);
        Icon.sprite = a.Icon;
        if (data.RewardGained)
        {
            Btn.interactable = false;
            Btn.GetComponent<Image>().color = Color.green;
            return;

        }
        if (data.achivmentDone)
        {
            Btn.GetComponent<Animator>().SetBool("Open", true);
        }
    }
    public void GetReward()
    {
        data.GainReward();
        Btn.interactable = false;
        Btn.GetComponent<Image>().color = Color.green;
        Btn.GetComponent<Animator>().SetBool("Open",false);

    }
}

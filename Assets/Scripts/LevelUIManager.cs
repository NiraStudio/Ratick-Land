using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {
    public Slider mainHpSlider,KeySlider;
    public Text CoinText,MainHpText,KeyAmount;

    LevelController LM;
    float coinTemp,lerp;
    int maxMainHp;
    public Character main;
	// Use this for initialization
	void Start () {
        LM = GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {


        #region Coin

        if (coinTemp !=LM.CoinAmount)
        {
            lerp += Time.deltaTime / 1;
            coinTemp = Mathf.Lerp(coinTemp, LM.CoinAmount, lerp);
        }
        else
            lerp = 0;

        CoinText.text = ((int)coinTemp).ToString();
        #endregion


        #region Main HP

        mainHpSlider.value = main.HP;
        MainHpText.text = main.HP + "/" + maxMainHp;

        #endregion

        #region Key

        KeyAmount.text = "X " + LM.keyCount + " Keys";
        KeySlider.maxValue = LM.KeyPartNeeded;
        KeySlider.value = LM.KeyPartGeted;

        #endregion
    }

    public void GetMain(Character main)
    {
        this.main = main;
        maxMainHp = this.main.HP;
        mainHpSlider.maxValue = maxMainHp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {

    public static LevelUIManager Instance;

    public Slider mainHpSlider,KeySlider;
    public Text CoinText,MainHpText,KeyAmount;
    public GameObject GoldBrust,GoldBrustTarget;
    LevelController LM;
    float coinTemp,lerp;
    public int maxMainHp;
    public Character main;

    void Awake()
    {
        Instance = this;
    }
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

        mainHpSlider.maxValue = maxMainHp;
        mainHpSlider.value = main.HP;
        MainHpText.text = main.HP + "/" + maxMainHp;

        #endregion

        #region Key

        KeyAmount.text = "X " + LM.keyCount + " Keys";
        KeySlider.maxValue = LM.KeyPartNeeded;
        KeySlider.value = LM.KeyPartGeted;

        #endregion
    }
    public void MakeGoldBrust(Vector2 Pos)
    {
        Instantiate(GoldBrust, Pos, Quaternion.identity).GetComponent<CollectableBrust>().make(GoldBrustTarget.transform,10);
    }

    public void GetMain(Character main)
    {
        this.main = main;
        maxMainHp = main.HP;
        mainHpSlider.maxValue = maxMainHp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class LevelUIManager : MonoBehaviour {

    public static LevelUIManager Instance;

    public Slider mainHpSlider,KeySlider;
    public LocalizedDynamicText MainHpText,KeyAmount,timerText;
    public GameObject GoldBrust,GoldBrustTarget;
    GamePlayManager GPM;
    KeyManager KM;
    float coinTemp,lerp;
    public int maxMainHp;
    public Character main;

    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        GPM = GetComponent<GamePlayManager>();
        KM = GetComponent<KeyManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        #region Timer
        double tt = GPM.remainingTime;
        string r = "";
        tt -= ((int)tt / 3600) * 3600;
        r += ((int)tt / 60).ToString("00") + ":";
        r += (tt % 60).ToString("00");
        timerText.text = r;
        #endregion

        #region Coin

        if (coinTemp !=GPM.CoinAmount)
        {
            lerp += Time.deltaTime / 1;
            coinTemp = Mathf.Lerp(coinTemp, GPM.CoinAmount, lerp);
        }
        else
            lerp = 0;

        #endregion


        #region Main HP
        if (main != null)
        {
            mainHpSlider.maxValue = maxMainHp;
            mainHpSlider.value = main.HP;
            MainHpText.text = main.HP + "/" + maxMainHp;
        }
        #endregion

        #region Key

        KeyAmount.text = "X " + KM.keyCount ;
        KeySlider.maxValue = KM.KeyPartNeeded;
        KeySlider.value = KM.KeyPartGeted;

        #endregion
    }
    public void MakeGoldBrust(Vector2 Pos,int amount)
    {
        Instantiate(GoldBrust, Pos, Quaternion.identity).GetComponent<CollectableBrust>().make(GoldBrustTarget.transform, amount);
    }

    public void GetMain(Character main)
    {
        this.main = main;
        maxMainHp = main.HP;
        mainHpSlider.maxValue = maxMainHp;
    }
}

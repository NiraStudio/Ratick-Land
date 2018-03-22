using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {

    public static LevelUIManager Instance;

    public Slider mainHpSlider,KeySlider;
    public Text CoinText,MainHpText,KeyAmount;
    public GameObject GoldBrust,GoldBrustTarget;
    LevelController LC;
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
        LC = GetComponent<LevelController>();
        KM = GetComponent<KeyManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {


        #region Coin

        if (coinTemp !=LC.CoinAmount)
        {
            lerp += Time.deltaTime / 1;
            coinTemp = Mathf.Lerp(coinTemp, LC.CoinAmount, lerp);
        }
        else
            lerp = 0;

        CoinText.text = ((int)coinTemp).ToString();
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

        KeyAmount.text = "X " + KM.keyCount + " Keys";
        KeySlider.maxValue = KM.KeyPartNeeded;
        KeySlider.value = KM.KeyPartGeted;

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

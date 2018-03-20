using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class HUDManager : MonoBehaviour {

    public LocalizedDynamicText CoinAmountText;

    GameManager GM;
    float coinTemp, lerp;
    // Use this for initialization
    void Start () {
        GM = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (coinTemp != GM.coinAmount)
        {
            lerp += Time.deltaTime / 1;
            coinTemp = Mathf.Lerp(coinTemp, GM.coinAmount, lerp);
        }
        else
            lerp = 0;

        CoinAmountText.Number = ((int)coinTemp).ToString();
    }
}

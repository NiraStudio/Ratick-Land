using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeSceneCardPanel : MonoBehaviour {
    public GameObject Panel;
    public Text CardDoubleAttackText, CardDoubleCoinText;


    GameManager GM;
    int CardDoubleAttack, CardDoubleCoin;
	// Use this for initialization
	void Start () {
        GM = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Open()
    {
        Panel.SetActive(true);
        CardDoubleAttack = CardDoubleCoin = 0;


        foreach (var item in GM.mainData.cardHolder.cards.ToArray())
        {
            switch (item.cardType)
            {
                case Card.Type.empty:
                    break;
                case Card.Type.DoubleCoin:
                    CardDoubleCoin++;
                    break;
                case Card.Type.DoubleATK:
                    CardDoubleAttack++;
                    break;

            }
        }

        CardDoubleAttackText.text = "X" + CardDoubleAttack;
        CardDoubleCoinText.text = "X" + CardDoubleCoin;
    }

    public void Close()
    {
        Panel.SetActive(false);

    }
    public void ChooseCard()
    {

    }
}

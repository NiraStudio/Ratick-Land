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


        foreach (var item in GM.mainData.porionHolder.cards.ToArray())
        {
            switch (item.cardType)
            {
                case Potion.Type.empty:
                    break;
                case Potion.Type.DoubleCoin:
                    CardDoubleCoin++;
                    break;
                case Potion.Type.DoubleATK:
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

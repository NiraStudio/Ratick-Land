using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {
    public Button BTN;
    public Reward reward;
	// Use this for initialization
	void Start () {
       // BTN.onClick.AddListener(ShowAd);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
            reward.MakeRandomAndGive();
        }
	}
    void ShowAd()
    {

    }

    [System.Serializable]
    public class Reward
    {
        public enum Type
        {
            Card,Coin,Icon
        }
        public IntRange CoinRandom=new IntRange(200,500);
        public IntRange CardRandom=new IntRange(1,2);
        public Type type;
        int amount;
        public void MakeRandomAndGive()
        {
            type =(Type) Random.Range(0, 2);
            switch (type)
            {
                case Type.Card:
                    amount = CardRandom.Random;
                    GameManager.instance.AddCard(new Card(amount));
                    break;
                case Type.Coin:
                    amount = CoinRandom.Random;
                    GameManager.instance.ChangeCoin(amount);
                    break;
                case Type.Icon:
                    break;
               
            }
        }

    }
}

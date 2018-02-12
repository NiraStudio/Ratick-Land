using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public Sprite[] Icons;
    public ChestCard[] Cards;
    public ChestCard VideoCard;
    Animator anim;
    bool open = false;

    ChestReward AdReward;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if (open)
            {
                anim.SetTrigger("Close");
                open = false;
            }
            else
            {
                MakeChest(3);
            }
        }
	}
    public void MakeChest(int RewardAmount)
    {
        ChestReward t = new ChestReward();
        for (int i = 0; i < RewardAmount; i++)
        {
            ///Algoritem
            t.icon = Icons[Random.Range(0,Icons.Length)];
            t.amount = Random.Range(10, 200);
            t.type = (ChestReward.Type)Random.Range(0, 4);
            print(t.type.ToString());
            Cards[i].Repaint(t);
            t.GainReward();
        }

        //AdCard
        anim.SetTrigger("Open");
        open = true;
    }

    [System.Serializable]
    public class Reward
    {
        public enum Type
        {
            Card, Coin, Icon
        }
        public IntRange CoinRandom = new IntRange(200, 500);
        public IntRange CardRandom = new IntRange(1, 2);
        public Type type;
        int amount;
        public void MakeRandomAndGive()
        {
            type = (Type)Random.Range(0, 2);
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

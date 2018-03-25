using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Potion {
    public enum Type
    {
        empty,DoubleCoin,DoubleATK
    }
    public Type cardType;
    public int cardAmount;


    public Potion(Type CardType,int cardAmount)
    {
        this.cardType = CardType;
        this.cardAmount = cardAmount;
    }
    public Potion(int cardAmount)
    {
        this.cardAmount = cardAmount;
        this.cardType = (Type) Random.Range(1, 3);
    }

    public void Action()
    {
        switch (cardType)
        {
            case Type.empty:
                break;
            case Type.DoubleCoin:
                Debug.Log("dOUBE Coin");
                LevelController.instance.WorldCoinMultiply = 2;
                break;
            case Type.DoubleATK:
                Debug.Log("dOUBE Atk");
                LevelController.instance.WorldAttackMultiPly = 2;

                break;
           
        }
    }
	
}
[System.Serializable]
public class PotionHolder
{
    public List<Potion> cards = new List<Potion>();
    public void Remove(Potion.Type type)
    {
        foreach (var item in cards.ToArray())
        {
            if(item.cardType==type)
            {
                item.cardAmount -= 1;
                if (item.cardAmount <= 0)
                    cards.Remove(item);
                break;
            }
        }
    }
}


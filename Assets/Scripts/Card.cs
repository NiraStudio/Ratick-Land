using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card {
    public enum Type
    {
        empty,DoubleCoin,DoubleATK
    }
    public Type cardType;
    public int cardAmount;


    public Card(Type CardType,int cardAmount)
    {
        this.cardType = CardType;
        this.cardAmount = cardAmount;
    }
    public Card(int cardAmount)
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
                //increase coin
                break;
            case Type.DoubleATK:
                //increase ATK
                break;
           
        }
    }
	
}
[System.Serializable]
public class CardHolder
{
    public List<Card> cards = new List<Card>();
}


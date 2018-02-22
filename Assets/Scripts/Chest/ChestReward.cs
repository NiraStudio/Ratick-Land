using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestReward : MonoBehaviour {
    public enum Type
    {
        UpgradeCard,Coin,Gem,Card
    }
    public Type type;
    public int amount;
    public Sprite icon;

    public int CharacterID
    {
        get { return _CharacterID; }
        set { _CharacterID = value; }
    }
    int _CharacterID;
    public void GainReward()
    {
        switch (type)
        {
            case Type.UpgradeCard:
                break;
            case Type.Coin:
                break;
            case Type.Gem:
                break;
            case Type.Card:
                break;
        }
        print(type);

    }

}

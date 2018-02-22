using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour {
    #region Singleton
    public static RewardManager Instance;

    void Awake()
    {
        Instance = this;
    }
    #endregion


    public Sprite CoinCard, DoubleAttackCard, DoubleCoinCard, GemCard;

    GameManager GM;
    CharacterDataBase characterDB;
    // Use this for initialization
    void Start () {
        GM = GetComponent<GameManager>();
        characterDB = GM.characterDB;
	}
	
    public RewardInfo AddReward(RewardType type, int amount)
    {
        RewardInfo t = new RewardInfo();
        t.amount = amount;

        switch (type)
        {
            case RewardType.UpgradeCard:
                CharacterData d = characterDB.GiveByRandom();
                t.Icon = d.icon;
                t.characterId = d.id;
                break;


            case RewardType.Coin:
                t.Icon = CoinCard;
                break;


            case RewardType.Gem:
                t.Icon = GemCard;
                break;


            case RewardType.Card:
                Card a = new Card((Card.Type)Random.Range(1, 3), amount);
                switch (a.cardType)
                {
                    case Card.Type.empty:
                        break;
                    case Card.Type.DoubleCoin:
                        t.Icon = DoubleCoinCard;
                        break;
                    case Card.Type.DoubleATK:
                        t.Icon = DoubleAttackCard;
                        break;
                }
                t.cardType = a.cardType;
                break;
            default:
                break;
        }
        t.type = type;
        return t;
    }
    public void AddReward(RewardInfo info)
    {
        switch (info.type)
        {
            case RewardType.UpgradeCard:
                GameManager.instance.AddCharacterCard(info.characterId,info.amount);
                break;
            case RewardType.Coin:
                GM.ChangeCoin(info.amount);
                break;
            case RewardType.Gem:
                GM.ChangeGem(info.amount);
                break;
            case RewardType.Card:
                GameManager.instance.AddCard(new Card(info.cardType, info.amount));
                break;
            default:
                break;
        }
    }
    
}
public enum RewardType
{
    UpgradeCard, Coin, Gem, Card
}
public class RewardInfo
{
    public Sprite Icon;
    public RewardType type;
    public Card.Type cardType;
    public int amount;
    public int characterId;
}

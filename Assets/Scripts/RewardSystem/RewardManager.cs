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
    public string CoinCardName, GemCardName, DoubleCoinName, DoubleAttackName;
    GameManager GM;
    CharacterDataBase characterDB;
    // Use this for initialization
    void Start () {
        GM = GetComponent<GameManager>();
        characterDB = GM.characterDB;
	}
	
    public RewardInfo MakeReward(RewardType type, int amount)
    {
        RewardInfo t = new RewardInfo();
        t.amount = amount;

        switch (type)
        {
            case RewardType.UpgradeCard:
                CharacterData d = characterDB.GiveByRandom();
                t.Icon = d.icon;
                t.characterId = d.id;
                t.RewardName = d.characterName;
                break;

            case RewardType.NewCharacter:
                CharacterData b = characterDB.GiveNewCharacter();
                t.Icon =b.icon;
                t.characterId = b.id;
                t.RewardName = b.characterName;
                break;

            case RewardType.Coin:
                t.Icon = CoinCard;
                t.RewardName = CoinCardName;
                break;


            case RewardType.Gem:
                t.Icon = GemCard;
                t.RewardName = GemCardName;
                break;


            case RewardType.Potion:
                Potion a = new Potion((Potion.Type)Random.Range(1, 3), amount);
                switch (a.cardType)
                {
                    case Potion.Type.empty:
                        break;
                    case Potion.Type.DoubleCoin:
                        t.Icon = DoubleCoinCard;
                        t.RewardName = DoubleCoinName;

                        break;
                    case Potion.Type.DoubleATK:
                        t.Icon = DoubleAttackCard;
                        t.RewardName = DoubleAttackName;
                        break;
                }
                t.potionType = a.cardType;
                t.amount = amount;
                
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
            case RewardType.NewCharacter:
                GameManager.instance.AddCharacterCard(info.characterId, info.amount);
                break;
            case RewardType.Potion:
                GameManager.instance.AddPotion(new Potion(info.potionType, info.amount));
                break;
            default:
                break;
        }
    }
    
}
public enum RewardType
{
    UpgradeCard, Coin, Gem, Potion,NewCharacter
}

[System.Serializable]
public class RewardInfo
{
    public string RewardName;
    public Sprite Icon;
    public RewardType type;
    public Potion.Type potionType;
    public int amount;
    public int characterId;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TapsellSDK;
using Alpha.Localization;

public class GameManager : MainBehavior
{
    public static GameManager instance;
    public SlotContainer SlotData
    {
        get { return mainData.defultSlot; }
        set
        {
            mainData.defultSlot = value;
            SaveMainData();
        }
    }


    public CharacterDataBase characterDB;
    public SkinDataBase skinDB;
    public StringDataBase RewardCodes;
    public SFX sfx;
    //Datas
    [SerializeField]
    MainData _mainData = new MainData();
    [SerializeField]
    CurrencyData _currencyData = new CurrencyData();

    public int coinAmount
    {
        get { return _currencyData.Coins; }
    }
    public MainData mainData
    {
        get { return _mainData; }
    }

    bool LoadFinished;
    // Use this for initialization

    void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        Tapsell.initialize("sonnarsnhngcetmrogmagklqsjhkqhbecchfearcsbdrqarifnpskfqlettjjedpnsjssr");
        if (PlayerPrefs.GetInt("FirstTime") != 1)
        {
            FirstTimeChanges();
        }
        else
        {
            LoadCurrencyData();
            LoadMainData();
        }
        StartCoroutine(PreCheck());
        yield return new WaitUntil(() => LoadFinished);
        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene("MainMenu");
        
    }

    // Update is called once per frame

    public void ChangeCoin(int Amount)
    {
        _currencyData.Coins += Amount;
        SaveCurrencyData();
    }
    public void ChangeGem(int Amount)
    {
        _currencyData.Gems += Amount;
        SaveCurrencyData();
    }

    public void AddCard(Card card)
    {
        _mainData.cardHolder.cards.Add(card);
        SaveMainData();
    }

    public void FirstTimeChanges()
    {
        _mainData = new MainData();
        _currencyData = new CurrencyData();
        mainData.characterInfos.Add(new characterInfo(2, 5, 0));
        mainData.characterInfos.Add(new characterInfo(1, 1, 0));
        _currencyData.Coins = 100;
        mainData.defultSlot.mainId = 1;
        SaveCurrencyData();
        SaveMainData();
        PlayerPrefs.SetInt("FirstTime", 1);
        PlayerPrefs.SetInt("FirstBoss", 1);
        PlayerPrefs.SetInt("BossKilled", 0);
        PlayerPrefs.SetInt("Tutorial", 1);

    }

    public void MakeCardFree()
    {
        SlotData.card = null;
        SaveMainData();
    }
    public IEnumerator PreCheck()
    {

        yield return new WaitUntil(()=> LocalizationManager.Instance.GetIsReady);
        yield return new WaitUntil(()=> sfx.IsReady);

        LoadFinished = true;
    }
    public string giveeRandomRewardCode()
    {
        return RewardCodes.GiveRandom;
    }

    #region Save/Load Methods

    public void SaveCurrencyData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
        }
        FileStream File = new FileStream(Application.persistentDataPath + "/Data/CR.Alpha", FileMode.Create);
        bf.Serialize(File, _currencyData);

        File.Close();
    }
    public void LoadCurrencyData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data/CR.Alpha"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/Data/CR.Alpha", FileMode.Open);

            _currencyData = bf.Deserialize(File) as CurrencyData;
            File.Close();
        }
        else
        {
            print("File not Exist");
            SaveCurrencyData();
        }
    }

    public void SaveMainData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
        }
        FileStream File = new FileStream(Application.persistentDataPath + "/Data/MD.Alpha", FileMode.Create);
        bf.Serialize(File, _mainData);

        File.Close();
    }
    public void LoadMainData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data/MD.Alpha"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/Data/MD.Alpha", FileMode.Open);

            _mainData = bf.Deserialize(File) as MainData;
            File.Close();
        }
        else
        {
            print("File not Exist");
            SaveMainData();
        }
    }

    #endregion


    #region Characters Methods
    public void RemoveCharacter(int id)
    {
        foreach (var item in mainData.characterInfos)
        {
            if (item.Id == id)
            {
                mainData.characterInfos.Remove(item);
                break;
            }
        }
        SaveMainData();
    }
    public void AddCharacter(int id,int level,int Card) 
    {
        mainData.characterInfos.Add(new characterInfo(id, level, Card));
        SaveMainData();

    }
    public void IncreaseCharacterLevel(int ID, int levelAmount)
    {
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if (item.Id == ID)
            {
                item.Level +=levelAmount;
                break;
            }
        }

        SaveMainData();
    }
    public int CharacterLevel(int ID)
    {
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if (item.Id == ID)
            {
                return item.Level;
            }
        }
        return -1;

    }
    public int CharacterUpgradeCost(CharacterData character)
    {
        int answer = 0;

        for (int i = 0; i < CharacterLevel(character.id); i++)
        {
            answer += character.upgradePrice.Amount;
        }

        return answer;
    }
    public int CharacterUpgradeCost(int ID)
    {
        int answer = 0;
        CharacterData a = characterDB.GiveByID(ID);

        for (int i = 0; i < CharacterLevel(ID); i++)
        {
            answer += a.upgradePrice.Amount;
        }

        return answer;
    }
    public int CharacterCardUpgradeCost(int ID)
    {
        int answer=0;
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if (item.Id == ID)
            {

                answer = characterDB.GiveByID(item.Id).baseCardNeed + (characterDB.GiveByID(item.Id).CardNeedIncrease * item.Level);
            }
        }
        return answer;

    }
    public void AddCharacterCard(int ID, int amount)
    {
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if (item.Id == ID)
            {
                item.cards += amount;
                SaveMainData();
                return;
            }
        }
        mainData.characterInfos.Add(new characterInfo(ID, 1, amount));
        SaveMainData();
    }
    public List<CharacterData> PlayerCharacters
    {
        get
        {
            List<CharacterData> answer = new List<CharacterData>();
            foreach (var item in mainData.characterInfos.ToArray())
            {
                answer.Add(characterDB.GiveByID(item.Id));
            }
            return answer;
        }
    }
    public int CharacterCard(int ID)
    {
        int answer = 0;
        foreach (var item in mainData.characterInfos)
        {
            if (item.Id == ID)
            {
                answer = item.cards;
                break;
            }
        }
        return answer;
    }
    public bool DoesPlayerHasThisCharacter(int ID)
    {
        bool answer = false;
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if(item.Id==ID)
            {
                answer = true;
                break;
            }
        }

        return answer;
    }
    public List<string> CharacterBoughtedSkins(int ID)
    {
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if (item.Id == ID)
                return item.BoughtedSkins;
        }
        return null;
    }
    public string CurrentCharacterSkin(int ID)
    {
        foreach (var item in mainData.characterInfos.ToArray())
        {
            if (item.Id == ID)
                return item.CurrentSkin;
        }
        return null;
    }
    public DetailState CharacterState(CharacterData Data)
    {
        DetailState state = new DetailState();

        float a,c,d;
        a = Data.damage;
        c = Data.attackSpeed;
        d= Data.hitPoint;

        for (int i = 0; i < CharacterLevel(Data.id); i++)
        {
            for (int j = 0; j < Data.UpgradesForEachLevel.Length; j++)
            {
                switch (Data.UpgradesForEachLevel[j].type)
                {
                   
                    case Upgrade.Type.Damage:
                        a += Data.UpgradesForEachLevel[j].amount;
                        break;
                    case Upgrade.Type.Hp:
                        d += Data.UpgradesForEachLevel[j].amount;

                        break;
                }
            }
            
        }
        state.AttackDamage =(int) a;
        state.AttackSpeed = c;
        state.HitPint =(int) d;
        return state;

    }
    #endregion







    void OnApplicationQuit()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            FirstTimeChanges();
        }
    }
}

[System.Serializable]
public class MainData
{
    public List<characterInfo> characterInfos = new List<characterInfo>();

    public CardHolder cardHolder = new CardHolder();
    public SlotContainer defultSlot = new SlotContainer();
}


[System.Serializable]
public class CurrencyData
{
    public ObscuredInt Coins=0;
    public ObscuredInt Gems=0;
}
[System.Serializable]
public class characterInfo
{
    public int Id;
    public int Level;
    public int cards;
    public string CurrentSkin = "Normal";
    public List<string> BoughtedSkins = new List<string>();
    public characterInfo(int Id,int level,int cards)
    {
        this.cards = cards;
        this.Id = Id;
        this.Level = level;
    }
}


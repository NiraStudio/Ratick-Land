using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MainBehavior {
    public static GameManager instance;
    [Tooltip("Restarts the game")]
    public bool Restart;
    public SlotContainer SlotData
    {
        get { return mainData.defultSlot; }
        set
        {
            mainData.defultSlot = value;
            SaveMainData();
        }
    }

    
    


    //Datas
    [SerializeField]
    MainData _mainData=new MainData();
    [SerializeField]
    CurrencyData currencyData=new CurrencyData();

    public int coinAmount
    {
        get { return currencyData.Coins; }
    }
    public MainData mainData
    {
        get { return _mainData; }
    }

	// Use this for initialization

    void Awake()
    {
        instance = this;
    }
	void Start () {
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.GetInt("FirstTime") != 1||Restart)
            FirstTimeChanges();
        else
        {
            LoadCurrencyData();
            LoadMainData();
            SceneManager.LoadScene("MainMenu");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeCoin(int Amount)
    {
        currencyData.Coins += Amount;
        SaveCurrencyData();
    }

    public void AddCard(Card card)
    {
        _mainData.cardHolder.cards.Add(card);
        SaveMainData();
    }

    public void FirstTimeChanges()
    {
        mainData.charactersData.Add(1, 5);
        mainData.charactersData.Add(5, 1);
        currencyData.Coins = 100;
        SaveCurrencyData();
        SaveMainData();
        PlayerPrefs.SetInt("FirstTime", 1);
        SceneManager.LoadScene("MainMenu");
        Restart = false;
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
        bf.Serialize(File, currencyData);

        File.Close();
    }
    public void LoadCurrencyData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data/CR.Alpha"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/Data/CR.Alpha", FileMode.Open);

            currencyData = bf.Deserialize(File) as CurrencyData;
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
    public void IncreaseLevel(int ID, int levelAmount)
    {
        mainData.charactersData[ID] = mainData.charactersData[ID] + levelAmount;
        SaveMainData();
    }
    public int CharacterLevel(int ID)
    {
        return mainData.charactersData[ID];
    }
    public int UpgradeCost(CharacterData character)
    {
        int answer = 0;

        for (int i = 0; i < CharacterLevel(character.id); i++)
        {
            answer += character.upgradePrice.Amount;
        }

        return answer;
    }
    

    #endregion
}

[System.Serializable]
public class MainData
{
    public Dictionary<int, int> charactersData=new Dictionary<int,int>();
    public CardHolder cardHolder = new CardHolder();
    public SlotContainer defultSlot = new SlotContainer();
}


[System.Serializable]
public class CurrencyData
{
    public ObscuredInt Coins=0;
}



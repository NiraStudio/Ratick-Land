using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
using Cinemachine;

[RequireComponent(typeof(LevelUIManager))]
[RequireComponent(typeof(KeyManager))]
[RequireComponent(typeof(GamePlayInput))]

public class GamePlayManager : MainBehavior
{
    #region Singleton
    public static GamePlayManager instance;
    void Awake()
    {
        instance = this;
    }
#endregion


    public CharacterDataBase characterDataBase;
    public float WorldCoinMultiply = 1, WorldAttackMultiPly = 1,WorldSpeedMultiPly=1;
    public float EnemyDamageMultiPly=1;
    public float MatchTime;
    public BGM bgm;
    public Transform StartPoint;
    public Wave[] WavePoints;
    public GamePlayState gameState;
    public int CoinAmount
    { get { return coinAmount; } }
    public int CharacterAmount
    {
        get { return characters.Count; }
    }


    List<GameObject> characters=new List<GameObject>();
    SlotContainer sc = new SlotContainer();
    GameObject aimer;
    CameraController cameraController;
    ObscuredInt coinAmount;
    GameManager GM;
    Vector2 t;
    int _CageBroken=-1;
    CinemachineConfiner cinemachine;

    [HideInInspector]
    public float remainingTime;


    public int BrokenCage
    {
        get { return _CageBroken; }
    }

   
    void Start()
    {
        //gameState = GamePlayState.Pause;
        //start Text

        GM = GameManager.instance;
        //Camera
        cameraController = CameraController.Instance;
        cinemachine = cameraController.GetComponent<CinemachineConfiner>();
        aimer = GetComponent<GamePlayInput>().aimer;
        remainingTime = MatchTime;

        StartSpawningEnemies();
        //character spawns
        sc = GM.SlotData;
        spawnCharacters();

        //making FirstCage



        //Use Card
        UsePotion(GM.SlotData.porion);

        //Starting the game
        OpenScreen();
        bgm = GetComponent<BGM>();
        bgm.PlaySound(BGM.State.Main);
        GameAnalyticsManager.SendCustomEvent("PlayGame");

    }

    void Update()
    {
        if (gameState == GamePlayState.Finish)
            return;

        remainingTime -= Time.deltaTime;
        
        if (remainingTime <= 0)
        {
            FinishTheGame("Victory");
        }
    }

    public void RemoveCharacter(GameObject character)
    {
        characters.Remove(character);
        cameraController.ChangeTargets(characters);
    }
    public void AddCharacters(GameObject character)
    {
        characters.Add(character);
        cameraController.ChangeTargets(characters);
    }
    void spawnCharacters()
    {
        GameObject p = new GameObject("Characters");
        p.transform.position = StartPoint.position;
        if (sc.mainId >= 0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.mainId);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position = StartPoint.position;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);
            GetComponent<LevelUIManager>().GetMain(a.GetComponent<Character>());
        }
        foreach (var item in sc.Heros)
        {
            CharacterData c = characterDataBase.GiveByID(item);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position = (Vector2)StartPoint.position + Random.insideUnitCircle * 1.5f;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);

        }
        if(sc.minionId>=0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.minionId);
            int a = GM.CharacterLevel(sc.minionId);
            for (int i = 0; i < a; i++)
            {
                GameObject b = Instantiate(c.prefab, p.transform);
                b.transform.position =(Vector2) StartPoint.position + Random.insideUnitCircle * 1.5f;
                b.GetComponent<Character>().Release(true);
                AddCharacters(b);
            }
        }
        if (sc.supportId >= 0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.supportId);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position =(Vector2) StartPoint.position + Random.insideUnitCircle * 1.5f;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);

        }

        

        
    }

    void StartSpawningEnemies()
    {
        for (int i = 0; i < WavePoints.Length; i++)
        {
            WavePoints[i].Spawn();
        }
        StartCoroutine(SpanwEnemy());
    }

    IEnumerator SpanwEnemy()
    {
        if (gameState != GamePlayState.Finish)
        {
            yield return new WaitForSeconds(10f);
            for (int i = 0; i < WavePoints.Length; i++)
            {
                WavePoints[i].Spawn();
            }
            StartCoroutine(SpanwEnemy());
        }
    }

    void UsePotion(Potion card)
    {
        if (card == null)
            return;
        card.Action();
        GM.mainData.porionHolder.Remove(card.cardType);
        GM.SlotData.porion = null;
        GM.SaveMainData();
        GameAnalyticsManager.SendCustomEvent(card.cardType.ToString());
    }   
    public void FinishTheGame(string State)
    {
        if (gameState != GamePlayState.Finish)
        {
            gameState = GamePlayState.Finish;
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
            foreach (var item in enemies)
            {
                item.Die();
            }
            GM.ChangeCoin(coinAmount);
            if (PlayerPrefs.GetInt("Tutorial") == 1)
            {
                PlayerPrefs.SetInt("Tutorial", 2);
                GM.RemoveCharacter(2);
                GM.AddCharacter(2, 5, 0);
                GM.RemoveCharacter(3);
                GM.RemoveCharacter(4);
                GM.RemoveCharacter(5);
                GM.SlotData = new SlotContainer();
                GM.SlotData.mainId = 1;
                GM.SaveMainData();
            }
            else
            {
                AchievementManager.Instance.Add(AchievementType.play, 1);
            }
            if (PlayerPrefs.GetInt("Played") < 5)
                PlayerPrefs.SetInt("Played", PlayerPrefs.GetInt("Played") + 1);
            InformationPanel.Instance.OpenFinshPanel(State,State=="Victory"?PanelColor.Succuss:PanelColor.Alert, coinAmount, () =>
              {
                  GoToScene("MainMenu");
                  print("Here");
              });
            
        }
        bgm.stopSound();
    }
   

    public void ChangeCoin(int Amount)
    {
        coinAmount += Amount;
       
    }
    

    










}


public enum GamePlayState
{
    Playing,Finish,Pause
}
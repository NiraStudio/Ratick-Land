using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
using Cinemachine;

[RequireComponent(typeof(LevelUIManager))]
[RequireComponent(typeof(KeyManager))]
[RequireComponent(typeof(GamePlayInput))]

public class LevelController : MainBehavior
{
    public static LevelController instance;
    public CharacterDataBase characterDataBase;
    public string PersianMissionText, EnglishMissionText;
    public int WorldCoinMultiply=1,WorldAttackMultiPly=1,maxWave;
    public float EnemyDamageMultiPly=1;
    public MissionTextBehaivior missionText;
    public BGM bgm;
    public GameObject PausePanel;
    [Range(0,100)]
    public float ChanceToRespawnSecretMap;

    [Header("Map Parameters")]
    public GameObject cage,Boss;
    public GameObject[] Blocks,Maps;
    public GameObject SecretMap,wave;
    public IntRange BlockAmount;

    public GamePlayState gameState;
    public int CoinAmount
    { get { return coinAmount; } }



    List<GameObject> currentWaves=new List<GameObject>();
    List<Vector2> WavePoints = new List<Vector2>();
    List<Vector2> BossSpots = new List<Vector2>();
    public List<Vector2> CagePoints = new List<Vector2>();
    List<GameObject> characters=new List<GameObject>();
    SlotContainer sc = new SlotContainer();
    GameObject aimer;
    CameraController cameraController;
    ObscuredInt coinAmount;
    GameManager GM;
    Vector2 t,startPos;
    Touch[] touches;
    int _CageBroken=-1;
    bool move;
    MapClass map;
    public bool Allow;
    CinemachineConfiner cinemachine;
    public int BrokenCage
    {
        get { return _CageBroken; }
    }

    void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        gameState = GamePlayState.Pause;
        //start Text
        
        GM=GameManager.instance;
        //Camera
        cameraController = CameraController.Instance;
        cinemachine = cameraController.GetComponent<CinemachineConfiner>();
        aimer = GetComponent<GamePlayInput>().aimer;
       
        //designing the map  (Blocks , map &...)
        designMap();
        sc = GM.SlotData;
        spawnCharacters();


        //making FirstCage
        MakeCage();
        
        //start Spawning the Waves
        StartCoroutine(SpawnWaves());


        //Use Card
        UseCard(GM.SlotData.porion);

        //Starting the game
        OpenScreen();
        bgm = GetComponent<BGM>();
        bgm.PlaySound(BGM.State.Main);
        yield return new WaitForSeconds(1);
        missionText.MakeText(PersianMissionText, EnglishMissionText);
        GameAnalyticsManager.SendCustomEvent("PlayGame");
        
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
    
    public void MakeCage()
    {
        Vector2 a;
        a = giveMapPos(10,CagePoints);
        GameObject g = Instantiate(cage,map.gameObject.transform);
        g.transform.localPosition = a;
        g.transform.SetParent(null);

        CageFinder.Instance.ChangeTarget(g);
        _CageBroken++;
        if(_CageBroken<=10)
        EnemyDamageMultiPly = 1 + (_CageBroken * 0.15f);
    }

    
    void spawnCharacters()
    {
        GameObject p = new GameObject("Characters");
        p.transform.position = startPos;
        if (sc.mainId >= 0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.mainId);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position = startPos;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);
            GetComponent<LevelUIManager>().GetMain(a.GetComponent<Character>());
        }
        foreach (var item in sc.Heros)
        {
            CharacterData c = characterDataBase.GiveByID(item);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position =startPos+ Random.insideUnitCircle * 1.5f;
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
                b.transform.position =startPos+ Random.insideUnitCircle * 1.5f;
                b.GetComponent<Character>().Release(true);
                AddCharacters(b);
            }
        }
        if (sc.supportId >= 0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.supportId);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position =startPos+ Random.insideUnitCircle * 1.5f;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);

        }

        

        
    }
    void designMap()
    {
        int a = Random.Range(0, 100);
        GameObject g;
        if (a < ChanceToRespawnSecretMap)
        {
            g = Instantiate(SecretMap, Vector2.zero, Quaternion.identity);
        }
        else
        {
            g = Instantiate(Maps[Random.Range(0, Maps.Length)], Vector2.zero, Quaternion.identity);
        }
        map = g.GetComponent<MapClass>();

        WavePoints = map.points(PointsType.Wave);
        BossSpots = map.points(PointsType.Boss);
        CagePoints = map.points(PointsType.Cage);

        startPos =map.startPoint.transform.position;
        GameObject.FindWithTag("Aim").transform.position = startPos;
        map.DestroyGameObjects();
        cinemachine.m_BoundingShape2D = map.Bounds;
        MakeBoss();
    }
    void MakeBoss()
    {
        Instantiate(Boss, BossSpots[Random.Range(0, BossSpots.Count)], Quaternion.identity);
    }

    void UseCard(Potion card)
    {
        if (card == null)
            return;
        card.Action();
        GM.mainData.porionHolder.Remove(card.cardType);
        GM.SlotData.porion = null;
        GM.SaveMainData();
        GameAnalyticsManager.SendCustomEvent(card.cardType.ToString());
    }
    Vector2 giveMapPos(List<Vector2> poses)
    {
        return poses[Random.Range(0, poses.Count)];
    }
    Vector2 giveMapPos(float distance,List<Vector2> poses)
    {
        Vector2 t = new Vector2();
         do
        {
            t = poses[Random.Range(0, poses.Count)];
        } while (Vector2.Distance(t, Camera.main.transform.position) < distance);
         return t;
    }

    
    IEnumerator SpawnWaves()
    {
        if (currentWaves.Count < maxWave)
        {
            while (currentWaves.Count < maxWave)
            {
                GameObject g = Instantiate(wave.gameObject, giveMapPos(7,WavePoints), Quaternion.identity);
               currentWaves.Add(g);
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnWaves());

    }
    public void FinishTheGame(string State)
    {
        if (gameState != GamePlayState.Finish)
        {
            gameState = GamePlayState.Finish;
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
              });
            
        }
        bgm.stopSound();
    }
    public void RemoveWave(GameObject go)
    {
        currentWaves.Remove(go);
    }


    public void ChangeCoin(int Amount)
    {
        coinAmount += Amount;
       
    }
    


    public void Pause()
    {
        PausePanel.SetActive(true);
        gameState = GamePlayState.Pause;
        GamePlayInput.Instance.JoyStickTurnOff();
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale =1;
        PausePanel.SetActive(false);
        gameState = GamePlayState.Playing;
    }
    public void Exit()
    {
        Time.timeScale = 1;
        FinishTheGame("Defeat");
    }










}


public enum GamePlayState
{
    Playing,Finish,Pause
}
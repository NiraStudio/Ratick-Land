using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;

[RequireComponent(typeof(LevelUIManager))]
[RequireComponent(typeof(KeyManager))]
[RequireComponent(typeof(GamePlayInput))]

public class LevelController : MainBehavior
{
    public static LevelController instance;
    public CharacterDataBase characterDataBase;
    public string PersianMissionText, EnglishMissionText;
    public int WorldCoinMultiply=1,WorldAttackMultiPly=1,maxWave;
    public MissionTextBehaivior missionText;
    public BGM bgm;
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
    List<Vector2> BlockSpots = new List<Vector2>();
    List<Vector2> BossSpots = new List<Vector2>();
    List<Vector2> CagePoints = new List<Vector2>();
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
        cameraController = Camera.main.GetComponent<CameraController>();
        aimer = GetComponent<GamePlayInput>().aimer;
        bgm = GetComponent<BGM>();
        bgm.PlaySound(BGM.State.Main);
        //designing the map  (Blocks , map &...)
        designMap();
        sc = GM.SlotData;
        spawnCharacters();


        //making FirstCage
        MakeCage();
        
        //start Spawning the Waves
        StartCoroutine(SpawnWaves());


        //Use Card
        UseCard(GM.SlotData.card);

        //Starting the game
        OpenScreen();
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
        a = giveMapPos(4,CagePoints);
        GameObject g = Instantiate(cage, a, Quaternion.identity);

        CageFinder.Instance.ChangeTarget(g);
        _CageBroken++;
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
        BlockSpots = map.points(PointsType.Block);
        BossSpots = map.points(PointsType.Boss);
        CagePoints = map.points(PointsType.Cage);

        startPos =map.startPoint.transform.position;
        GameObject.FindWithTag("Aim").transform.position = startPos;
        map.DestroyGameObjects();
        //Blocks = map.blocks;
       // MakeBlocks();
        MakeBoss();
    }
    void MakeBoss()
    {
        Instantiate(Boss, BossSpots[Random.Range(0, BossSpots.Count)], Quaternion.identity);
    }
    void MakeBlocks()
    {
        Vector2 tt;
        GameObject a = new GameObject();
        a.name = "Blocks";
        for (int i = 0; i < BlockAmount.Random; i++)
        {
            tt=BlockSpots[Random.Range(0,BlockSpots.Count)];
            Instantiate(Blocks[Random.Range(0, Blocks.Length)], tt, Quaternion.identity).transform.SetParent(a.transform);
            BlockSpots.Remove(tt);
        }
    }

    void UseCard(Card card)
    {
        if (card == null)
            return;
        card.Action();
        GM.mainData.cardHolder.Remove(card.cardType);
        GM.SlotData.card = null;
        GM.SaveMainData();
        GameAnalyticsManager.SendCustomEvent(card.cardType.ToString());
    }
    Vector2 giveMapPos()
    {
        return WavePoints[Random.Range(0, WavePoints.Count)];
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

            InformationPanel.Instance.OpenFinshPanel(State, coinAmount, () =>
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
    











    
    
}


public enum GamePlayState
{
    Playing,Finish,Pause
}
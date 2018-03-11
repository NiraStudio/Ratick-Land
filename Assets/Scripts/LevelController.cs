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
    
    public int WorldCoinMultiply=1,WorldAttackMultiPly=1,maxWave;


    [Range(0,100)]
    public float ChanceToRespawnSecretMap;

    [Header("Map Parameters")]
    public GameObject cage;
    public GameObject[] Blocks,Maps;
    public GameObject SecretMap,wave;
    public IntRange BlockAmount;

    public GamePlayState gameState;
    public int CoinAmount
    { get { return coinAmount; } }



    List<GameObject> currentWaves=new List<GameObject>();
    List<Vector2> freeSpots = new List<Vector2>();
    List<Vector2> BlockSpots = new List<Vector2>();
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
    
   
    public int BrokenCage
    {
        get { return _CageBroken; }
    }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GM=GameManager.instance;
        //Camera
        cameraController = Camera.main.GetComponent<CameraController>();
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
        UseCard(GM.SlotData.card);

        //Starting the game
        OpenScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (characters.Count <= 0)
            FinishTheGame();
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
    public void AddCharacter()
    {
        GameObject g = Instantiate(characterDataBase.GiveByID(1).prefab);
        g.transform.position = Random.insideUnitCircle * 3;
        g.GetComponent<Character>().Release(true);
        AddCharacters(g);

    }
    public void MakeCage()
    {
        Vector2 a;
        a = giveMapPos(10);
        GameObject g = Instantiate(cage, a, Quaternion.identity);

        CageFinder.Instance.ChangeTarget(g);
        _CageBroken++;
    }

    
    void spawnCharacters()
    {
        GameObject p = new GameObject("Characters");
        p.transform.position = startPos;
        foreach (var item in sc.Heros)
        {
            CharacterData c = characterDataBase.GiveByID(item);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position =startPos+ Random.insideUnitCircle * 3;
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
                b.transform.position =startPos+ Random.insideUnitCircle * 3;
                b.GetComponent<Character>().Release(true);
                AddCharacters(b);
            }
        }
        if (sc.supportId >= 0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.supportId);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position =startPos+ Random.insideUnitCircle * 3;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);

        }

        if (sc.mainId >= 0)
        {
            CharacterData c = characterDataBase.GiveByID(sc.mainId);
            GameObject a = Instantiate(c.prefab, p.transform);
            a.transform.position =startPos+ Random.insideUnitCircle * 3;
            a.GetComponent<Character>().Release(true);
            AddCharacters(a);
            GetComponent<LevelUIManager>().GetMain(a.GetComponent<Character>());

        }
        aimer.transform.position = startPos;
        
    }
    void designMap()
    {
        int a = Random.Range(0, 100);
        GameObject g;
        Vector3 r=new Vector3(0, 0, Random.Range(0, 360));
        if (a < ChanceToRespawnSecretMap)
        {
            g = Instantiate(SecretMap, Vector2.zero, Quaternion.Euler(r));
        }
        else
        {
            g = Instantiate(Maps[Random.Range(0, Maps.Length)], Vector2.zero, Quaternion.identity);
        }
        map = g.GetComponent<MapClass>();

        freeSpots = map.points(PointsType.Free);
        BlockSpots = map.points(PointsType.Block);
        startPos =map.startPoint.transform.position;
        map.DestroyGameObjects();
        MakeBlocks();
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
    }
    Vector2 giveMapPos()
    {
        return freeSpots[Random.Range(0, freeSpots.Count)];
    }
    Vector2 giveMapPos(float distance)
    {
        Vector2 t = new Vector2();
         do
        {
            t =freeSpots[Random.Range(0, freeSpots.Count)];
        } while (Vector2.Distance(t, Camera.main.transform.position) < distance);
         return t;
    }
    IEnumerator SpawnWaves()
    {
        if (currentWaves.Count < maxWave)
        {
            while (currentWaves.Count < maxWave)
            {
                GameObject g = Instantiate(wave.gameObject, giveMapPos(7), Quaternion.identity);
                print("Hello");
               currentWaves.Add(g);
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnWaves());

    }
    public void FinishTheGame()
    {
        gameState = GamePlayState.Finish;
        print("GameFinished");
        GM.ChangeCoin(coinAmount);
        GoToScene("MainMenu");

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
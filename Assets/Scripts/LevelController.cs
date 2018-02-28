using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
public class LevelController : MainBehavior
{
    public static LevelController instance;
    public CharacterDataBase characterDataBase;
    public CameraController cameraController;
    public CageFinder cageFinder;
    public JoyStick joyStick;
    public GameObject aimer;
    public float speedMultiPly;
    public int WorldCoinMultiply=1,WorldAttackMultiPly=1,maxWave;
    [Range(0,100)]
    public int ChanceToRespawnSecretMap;


    [Tooltip("Map Parameters")]
    public GameObject cage;
    public GameObject[] Blocks,Maps;
    public GameObject SecretMap,wave;
    public IntRange BlockAmount;
    public List<GameObject> currentWaves=new List<GameObject>();
    public bool Move
    {
        get { return move; }
    }
    public bool game;
    public int CoinAmount
    { get { return coinAmount; } }



    ObscuredInt coinAmount;

    int _CageBroken=-1;
    Dictionary<int, int> data = new Dictionary<int, int>();
    List<Vector2> freeSpots = new List<Vector2>();
    List<Vector2> BlockSpots = new List<Vector2>();
    List<GameObject> characters=new List<GameObject>();
    SlotContainer sc = new SlotContainer();
    GameManager GM;
    /// cach var
    Vector2 t,startPos;
    Touch[] touches;
    bool move;
    MapClass map;
    public float LevelTime
    {
        get { return levelTime; }
    }
    //key
    public int keyCount=3, GotedKey;
    public int KeyPartGeted, KeyPartNeeded;
    public int BrokenCage
    {
        get { return _CageBroken; }
    }

    float waveCount;
    float coinTemp,lerp=0,levelTime;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GM=GameManager.instance;
        designMap();
        data.Add(1, 50);
        sc = GM.SlotData;
        spawnCharacters();
        MakeCage();
        
        StartCoroutine(spawnEnemy());
        RenewKeyPartNeeded();
        UseCard(GM.SlotData.card);
        OpenScreen();
    }

    // Update is called once per frame
    void Update()
    {

        levelTime += Time.deltaTime;
        waveCount = maxWave +(int)( levelTime / 10);
        #region inputs
        if (Application.isMobilePlatform)
        {
            touches = Input.touches;
            if (touches.Length == 1)
                switch (touches[0].phase)
                {
                    case TouchPhase.Began:
                        JoyStickTurnOn(Camera.main.ScreenToWorldPoint(touches[0].position));
                        break;
                    case TouchPhase.Ended:
                        JoyStickTurnOff();
                        break;
                }
        }
        else if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
                JoyStickTurnOn(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetMouseButtonUp(0))
                JoyStickTurnOff();
        }
        #endregion

        #region characterMoves
        if (move)
        {

            t = aimer.transform.position;
            t.x += joyStick.direction.x * (joyStick.speed * speedMultiPly) * Time.deltaTime;
            t.y += joyStick.direction.y * (joyStick.speed * speedMultiPly) * Time.deltaTime;
            aimer.transform.position = t;

        }
        #endregion

        #region Key

        if(KeyPartGeted>=KeyPartNeeded)
        {
            ChangeKeyCount(1);
            RenewKeyPartNeeded();
        }

        #endregion
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

        do
        {
            a = giveMapPos();
        } while (Vector2.Distance(a, Camera.main.transform.position) < 10);


        GameObject g = Instantiate(cage, a, Quaternion.identity);

        cageFinder.cage = g;
        _CageBroken++;
    }

    void JoyStickTurnOn(Vector2 pos)
    {
        joyStick.gameObject.SetActive(true);
        joyStick.transform.position = pos;
        move = true;
    }
    void JoyStickTurnOff()
    {
        joyStick.gameObject.SetActive(false);
        move = false;
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
        for (int i = 0; i < BlockAmount.Random; i++)
        {
            tt=BlockSpots[Random.Range(0,BlockSpots.Count)];
            Instantiate(Blocks[Random.Range(0, Blocks.Length)], tt, Quaternion.identity);
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
    IEnumerator spawnEnemy()
    {
        if (currentWaves.Count < waveCount)
        {
            while (currentWaves.Count < waveCount)
            {
                GameObject g = Instantiate(wave.gameObject, giveMapPos(7), Quaternion.identity);
                g.GetComponent<Wave>().LC = this;
               currentWaves.Add(g);
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(spawnEnemy());

    }
    public void FinishTheGame()
    {
        game = false;
        print("GameFinished");
        GM.ChangeCoin(coinAmount);
        Application.LoadLevel(0);

    }



    public void ChangeCoin(int Amount)
    {
        coinAmount += Amount;
       
    }

    public void RenewKeyPartNeeded()
    {
        KeyPartGeted = KeyPartGeted - KeyPartNeeded;
        ChangeKeyPartNeeded();
    }
    public void ChangeKeyPartNeeded()
    {
        KeyPartNeeded = 0;
        KeyPartNeeded = 15 + (GotedKey * 15);
    }
    public void ChangeKeyCount(int amount)
    {
        keyCount += amount;
        if (amount > 0)
            GotedKey += amount;
    }

    public void ChangeKeyPart(int amount)
    {
        KeyPartGeted += amount;
    }
}

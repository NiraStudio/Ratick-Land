using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MainBehavior
{
    public static LevelController instance;
    public CharacterDataBase characterDataBase;
    public CameraController cameraController;
    public CageFinder cageFinder;
    public JoyStick joyStick;
    public GameObject aimer;
    public float speedMultiPly;
    public int WorldCoinMultiply=1,maxWave;
    [Range(0,100)]
    public int ChanceToRespawnSecretMap;


    [Tooltip("Map Parameters")]
    public GameObject cage;
    public GameObject[] Blocks,Maps;
    public GameObject SecretMap,wave;
    public List<GameObject> currentWaves=new List<GameObject>();
    public bool Move
    {
        get { return move; }
    }
    public bool game;




    Dictionary<int, int> data = new Dictionary<int, int>();
    List<Vector2> freeSpots=new List<Vector2>();
    List<GameObject> characters=new List<GameObject>();
    /// cach var
    Vector2 t;
    Touch[] touches;
    bool move;
    MapClass map;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        designMap();
        data.Add(1, 50);
        spawnCharacters();
        MakeCage();
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
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
        foreach (var item in data)
        {
            CharacterData c = characterDataBase.GiveByID(item.Key);
            switch (c.type)
            {
                case CharacterData.Type.Soldier:
                    for (int i = 0; i < item.Value; i++)
                    {
                        GameObject b = Instantiate(c.prefab,p.transform);
                        b.transform.position = Random.insideUnitCircle * 3;
                        b.GetComponent<Character>().Release(true);
                        AddCharacters(b);
                    }

                    break;
                case CharacterData.Type.Hero:
                    GameObject a = Instantiate(c.prefab,p.transform);
                    a.transform.position = Random.insideUnitCircle * 3;
                    a.GetComponent<Character>().Release(true);
                    AddCharacters(a);


                    break;

            }

        }
        
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
            g = Instantiate(Maps[Random.Range(0, Maps.Length)], Vector2.zero, Quaternion.Euler(r));
        }
        map = g.GetComponent<MapClass>();

        freeSpots = map.points(PointsType.Free);

        map.DestroyGameObjects();
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
        if (currentWaves.Count < maxWave)
        {
            while (currentWaves.Count < maxWave)
            {
                GameObject g = Instantiate(wave.gameObject, giveMapPos(7), Quaternion.identity);
                g.GetComponent<Wave>().controller = this;
               currentWaves.Add(g);
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(spawnEnemy());

    }
    void FinishTheGame()
    {
        game = false;
        print("GameFinished");
        Application.LoadLevel(0);

    }

    

}

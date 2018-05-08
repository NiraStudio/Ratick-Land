using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class XpController : MonoBehaviour {


    #region Singleton
    public static XpController Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public Slider XpBar;
    public LocalizedDynamicText levelText;
    public GameObject UpdatePanel;
    public float AttackIncrease, MinionAmount;
    int XpNeed;
    int xp;
    int UpdateAmount;
    int Lvl = 1;
    // Use this for initialization
    void Start () {
        calculateXp();
	}
	
	// Update is called once per frame
	void Update () {
        levelText.Number = Lvl.ToString();
	}
    public void Increase(int Type)
    {
        XpUpgrade u = (XpUpgrade)Type;
        print(u.ToString());
        switch (u)
        {
            case XpUpgrade.Minion:
                MinionAmount += 1;
                GameObject c = GameManager.instance.characterDB.GiveByID(2).prefab;
                for (int i = 0; i < 5; i++)
                {
                    Vector2 a = Vector2.zero;
                    a = Random.insideUnitCircle;
                    a = (Vector2)Camera.main.transform.position + a;
                    GameObject g = Instantiate(c, a, Quaternion.identity);
                    g.GetComponent<Character>().Release(true);
                    GamePlayManager.instance.AddCharacters(g);
                }
                break;
            case XpUpgrade.FullHp:
                Character[] s = GameObject.FindObjectsOfType<Character>();
                foreach (var item in s)
                {
                    item.RecoverHp();
                }
                break;
            case XpUpgrade.IncreaseAttack:
                AttackIncrease += 0.25f;
                break;
           
        }

        UpdateAmount--;
        if (UpdateAmount == 0)
            UpdatePanel.SetActive(false);
    }
    public void calculateXp()
    {
        XpNeed = Lvl * 7;
        XpBar.maxValue = XpNeed;
        XpBar.value = xp;
    }
    public void AddXp(int amount)
    {
        xp += amount;
        if (xp>=XpNeed)
        {
            xp -= XpNeed;
            calculateXp();
            UpdateAmount++;
            Lvl++;
            UpdatePanel.SetActive(true);

        }
        XpBar.value = xp;
    }
}
public enum XpUpgrade
{
    Minion,FullHp,IncreaseAttack
}

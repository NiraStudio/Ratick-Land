using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelScript : MonoBehaviour {
    public static InventoryPanelScript Instance;
    public CharacterDataBase characterDB;
    public GameObject CharacterBtnSample,CharacterPanelParent;
    public Text CardDoubleCoinText, CardDoubleAttackText;
    GameManager GM;
    Dictionary<int, int> charactersData;
    int CardDoubleCoin, CardDoubleAttack;
    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        GM = GameManager.instance;
        Close();
	}
	
	// Update is called once per frame
	
    public void RepaintCharacterPanel()
    {
        for (int i = 0; i < CharacterPanelParent.transform.childCount; i++)
        {
            Destroy(CharacterPanelParent.transform.GetChild(i).gameObject);
        }
        charactersData = GM.mainData.charactersData;
        foreach (var item in charactersData)
        {
            Instantiate(CharacterBtnSample, CharacterPanelParent.transform).GetComponent<CharacterPanelBtn>().Repaint(characterDB.GiveByID(item.Key));
        }
    }
    public void RepaintCardPanel()
    {
        CardDoubleAttack = CardDoubleCoin = 0;


        foreach (var item in GM.mainData.cardHolder.cards.ToArray())
        {
            switch (item.cardType)
            {
                case Card.Type.empty:
                    break;
                case Card.Type.DoubleCoin:
                    CardDoubleCoin++;
                    break;
                case Card.Type.DoubleATK:
                    CardDoubleAttack++;
                    break;
                
            }
        }

        CardDoubleAttackText.text = "X" + CardDoubleAttack;
        CardDoubleCoinText.text = "X" + CardDoubleCoin;


    }
    public void RepaintAll()
    {
        RepaintCardPanel();
        RepaintCharacterPanel();
    }

    public void Open()
    {
        RepaintAll();
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}

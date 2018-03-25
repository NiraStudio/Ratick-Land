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
    List<characterInfo> charactersData;
    int CardDoubleCoin, CardDoubleAttack;
    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        GM = GameManager.instance;
        Open();
	}
	
	// Update is called once per frame
	
    public void RepaintCharacterPanel()
    {
        
        for (int i = 0; i < CharacterPanelParent.transform.childCount; i++)
        {
            Destroy(CharacterPanelParent.transform.GetChild(i).gameObject);
        }
        charactersData = GM.mainData.characterInfos;
        foreach (var item in charactersData)
        {
            Instantiate(CharacterBtnSample, CharacterPanelParent.transform).GetComponent<CharacterPanelBtn>().Repaint(characterDB.GiveByID(item.Id));
        }
    }
    public void RepaintCardPanel()
    {
        CardDoubleAttack = CardDoubleCoin = 0;


        foreach (var item in GM.mainData.porionHolder.cards.ToArray())
        {
            switch (item.cardType)
            {
                case Potion.Type.empty:
                    break;
                case Potion.Type.DoubleCoin:
                    CardDoubleCoin++;
                    break;
                case Potion.Type.DoubleATK:
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

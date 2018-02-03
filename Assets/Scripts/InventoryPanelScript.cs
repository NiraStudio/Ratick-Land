using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelScript : MonoBehaviour {
    public static InventoryPanelScript Instance;
    public CharacterDataBase characterDB;
    public GameObject CharacterBtnSample,CharacterPanelParent;

    GameManager GM;
    Dictionary<int, int> charactersData;
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
            Destroy(CharacterPanelParent.transform.GetChild(0).gameObject);
        }
        charactersData = GM.mainData.charactersData;
        foreach (var item in charactersData)
        {
            Instantiate(CharacterBtnSample, CharacterPanelParent.transform).GetComponent<CharacterPanelBtn>().Repaint(characterDB.GiveByID(item.Key));
        }
    }
    public void RepaintCardPanel()
    {

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

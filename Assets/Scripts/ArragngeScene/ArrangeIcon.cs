using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeIcon : MonoBehaviour {

    public int ID,Level;
    public CharacterData.Type type;
    public Image img;
    public Text characterName,LevelText;
    CharacterData data;
    ArrangeSceneManager manager;
    void Start()
    {
        manager = ArrangeSceneManager.Instance;
        GetComponent<Button>().onClick.AddListener(Choosed);
        //img = GetComponent<Image>();
        //Clean();
    }

    void Choosed()
    {
        manager.OpenCharacterChoosePanel(this);
    }
    public void Repaint(CharacterData data)
    {
        this.data = data;
        img.sprite = data.icon;
        ID = data.id;
        characterName.text = data.characterName;
        LevelText.text = "Level :"+ GameManager.instance.CharacterLevel(ID);
    }
    public void Clean()
    {
        img.sprite = null;
        data = null;
        ID = -1;
        characterName.text = "";
        LevelText.text = "";
    }

}

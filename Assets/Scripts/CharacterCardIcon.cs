using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CharacterCardIcon : MonoBehaviour {
    public string CharacterName;
    public Image icon;
    public CharacterData data;
    public LocalizedKeyText characterName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame

    public void Repaint(CharacterData data)
    {
        icon.sprite = data.icon;
        CharacterName = data.name;
        this.data = data;
        characterName.Key = data.characterName;

    }
    public void Enable()
    {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }
    public void Clicked()
    {
        ArrangeSceneManager.Instance.CloseCharacterChoosePanel(data);
    }
}

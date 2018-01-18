using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeChooseButton : MonoBehaviour {
    public Image icon;
    public string CharacterName;
    public CharacterData data;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Clicked);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Repaint(CharacterData data)
    {
        icon.sprite = data.icon;
        CharacterName = data.name;
        this.data = data;
    }
    void Clicked()
    {
        ArrangeSceneManager.Instance.CloseChoosePanel(data);
    }
}

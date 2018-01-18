using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeIcon : MonoBehaviour {

    public int ID,Level;
    public CharacterData.Type type;
    public Image img;
    CharacterData data;
    ArrangeSceneManager manager;
    void Start()
    {
        manager = ArrangeSceneManager.Instance;
        GetComponent<Button>().onClick.AddListener(Choosed);
        img = GetComponent<Image>();
    }

    void Choosed()
    {
        manager.OpenChoosePanel(this);
    }
    public void Repaint(CharacterData data)
    {
        this.data = data;
        img.sprite = data.icon;
        ID = data.id;
    }

}

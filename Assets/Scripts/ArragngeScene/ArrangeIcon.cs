using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class ArrangeIcon : MonoBehaviour {

    public int ID,Level;
    public CharacterData.Type type;
    public LocalizedKeyText characterName;
        public LocalizedDynamicText LevelText;
    public ParticleSystem PS;
    public GameObject RespawnPoint;
    CharacterData data;
    ArrangeSceneManager manager;
    void Start()
    {
        manager = ArrangeSceneManager.Instance;
        if(GetComponent<Button>())
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
        for (int i = 0; i < RespawnPoint.transform.childCount; i++)
        {
            Destroy(RespawnPoint.transform.GetChild(i).gameObject);
        }
        GameObject g = Instantiate(data.prefab, RespawnPoint.transform.position, Quaternion.identity);
        Vector3 aa = g.transform.localScale;
        g.transform.SetParent(RespawnPoint.transform);
        g.GetComponent<Character>().enabled = false;
        g.transform.localScale = aa;
        ID = data.id;
        characterName.Key = data.characterName;
        Level= GameManager.instance.CharacterLevel(ID); ;
        LevelText.Text("مرحله " + GameManager.instance.CharacterLevel(ID), "Level "+ GameManager.instance.CharacterLevel(ID));
        PS.gameObject.SetActive(true);

    }

    public void Repaint(CharacterData data,int level)
    {
        this.data = data;
        for (int i = 0; i < RespawnPoint.transform.childCount; i++)
        {
            Destroy(RespawnPoint.transform.GetChild(i).gameObject);
        }
        GameObject g = Instantiate(data.prefab, RespawnPoint.transform.position, Quaternion.identity);
        Vector3 aa = g.transform.localScale;
        g.transform.SetParent(RespawnPoint.transform);
        g.GetComponent<Character>().enabled = false;
        g.transform.localScale = aa;
        ID = data.id;
        characterName.Key = data.characterName;
        this.Level = level;
        LevelText.Text("مرحله " + level, "Level " + level);

    }
    public void Clean()
    {
        for (int i = 0; i < RespawnPoint.transform.childCount; i++)
        {
            Destroy(RespawnPoint.transform.GetChild(i).gameObject);
        }
        data = null;
        ID = -1;
        characterName.Key = "";
        LevelText.Text ( "","");
    }

}

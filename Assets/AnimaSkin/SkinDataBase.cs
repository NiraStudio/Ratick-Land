using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDataBase :ScriptableObject  {
    public List<SkinData> DB = new List<SkinData>();

    public void AddSkin(SkinData data)
    {
        DB.Add(data);
    }
    public List<SkinData> CharacterBoughtedSkins(int ID)
    {
        List<SkinData> d = new List<SkinData>();
        foreach (var item in DB.ToArray())
        {
            if (item.CharacterID == ID)
                d.Add(item);
        }
        return d;
    }

    public void CleanDataBase()
    {
        DB = new List<SkinData>();
    }
}
[System.Serializable]
public class SkinData
{
    public string SkinName;
    public int CharacterID;
    public Sprite Icon;
    public Currency Price = new Currency();
}

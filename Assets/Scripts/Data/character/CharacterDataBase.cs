using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBase : ScriptableObject {

    public List<CharacterData> DataBase = new List<CharacterData>();
    public int Length
    {
        get { return DataBase.Count; }
    }
    public CharacterData GiveByID(int id)
    {
        CharacterData data = new CharacterData();
        bool found=false;
        foreach (var item in DataBase.ToArray())
        {
            if (item.id == id)
            {
                data = item;
                found = true;
                break;
            }
        }
        if (found)
            return data;
        else
            return null;
    }
    public CharacterData GiveByIndex(int i)
    {
        return DataBase[i];
    }
    public void AddCharacter(CharacterData data)
    {
        DataBase.Add(data);
    }
    public void RemoveCharacter(CharacterData data)
    {
        DataBase.Remove(data);
    }
}

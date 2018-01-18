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
        setDirty();
    }
    public void RemoveCharacter(CharacterData data)
    {
        DataBase.Remove(data);
        setDirty();

    }

    public List<CharacterData> GiveByType(CharacterData.Type type)
    {
        List<CharacterData> answer = new List<CharacterData>();
        foreach (var item in DataBase)
        {
            if (item.type == type)
                answer.Add(item);
        }
        return answer;
    }
    public CharacterData.Type giveCharacterMode(int id)
    {
        return GiveByID(id).type;
    }
    void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

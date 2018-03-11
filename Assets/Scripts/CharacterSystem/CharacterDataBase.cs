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
    public CharacterData GiveByRandom()
    {
        CharacterData d = new CharacterData();
        do
        {
            d = DataBase[Random.Range(0, DataBase.Count)];
        } while (GameManager.instance.DoesPlayerHasThisCharacter(d.id)==false);
        return d;
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
    public CharacterData GiveNewCharacter()
    {
        CharacterData d = null;
        List<CharacterData> data = new List<CharacterData>();
        foreach (var item in DataBase.ToArray())
        {
            if (!GameManager.instance.DoesPlayerHasThisCharacter(item.id))
            {
                data.Add(item);
            }
        }
        if (data.Count > 0)
        {
            d = data[Random.Range(0, data.Count)];
        }
        else
            d = DataBase[Random.Range(0, data.Count)];

        return d;
    }
    void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

}

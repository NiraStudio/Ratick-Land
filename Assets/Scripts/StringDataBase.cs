using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringDataBase : ScriptableObject {

    public List<string> DB = new List<string>();


    public bool contains(string text)
    {
        bool answer = false;
        foreach (var item in DB.ToArray())
        {
            if(item.ToLower()==text.ToLower())
            {
                answer = true;
                break;
            }
        }
        return answer;
    }
}

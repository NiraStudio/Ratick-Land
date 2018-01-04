using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Currency 
{
    public int Amount;
    public Type type;

    public enum Type
    {
        coin
    }
}


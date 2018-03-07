using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[System.Serializable]
[RequireComponent(typeof(SkinDB))]
public class Skin : MonoBehaviour
{
    
    [SerializeField]
    public string skinName;
    public Upgrade[] Attributes;
    public Currency Price = new Currency();
    [HideInInspector]
    [SerializeField]
    public List<SkinPart> skinParts;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[System.Serializable]
[RequireComponent(typeof(SkinDataBase))]
public class Skin : MonoBehaviour
{
    
    [SerializeField]
    public string skinName;
    public Sprite Icon;
    public Upgrade[] Attributes;
    public Currency Price = new Currency();
    [HideInInspector]
    [SerializeField]
    public List<SkinPart> skinParts;
}


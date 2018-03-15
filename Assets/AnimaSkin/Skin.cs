using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[System.Serializable]
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

    public DetailState State()
    {
        DetailState state = new DetailState();
        foreach (var item in Attributes)
        {
            switch (item.type)
            {
                case Upgrade.Type.Damage:
                    state.AttackDamage = item.amount;
                    break;
                case Upgrade.Type.Hp:
                    state.HitPint = item.amount;

                    break;

            }
        }
        return state;
    }
}


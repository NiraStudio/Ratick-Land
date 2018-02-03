using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade {
    public enum Type { MinDamage, MaxDamage,Damage, Hp };

    public Type type;

    public int amount;
}

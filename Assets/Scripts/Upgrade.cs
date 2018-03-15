using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade {
    public enum Type { Damage, Hp };

    public Type type;

    public int amount;
}

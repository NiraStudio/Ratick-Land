using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData:ScriptableObject
{
    public string characterName;
    public GameObject prefab;
    public Currency upgradePrice = new Currency();
    public int baseCardNeed,CardNeedIncrease;
    public Type type;
    public int id;
    public int maxLevel;
    public Sprite icon = null;
    public float speed=3;
    public float attackSpeed;
    public float hitPoint;
    public float attackRange;
    public int damage;
    public Upgrade[] UpgradesForEachLevel;
    public string description;


    public CharacterData(string characterName, GameObject prefab, int id, Sprite icon, float speed, float attackSpeed, float hitPoint, int damage, string description)
    {
        this.characterName = characterName;
        this.prefab = prefab;
        this.id = id;
        this.icon = icon;
        this.speed = speed;
        this.attackSpeed = attackSpeed;
        this.hitPoint = hitPoint;
        this.damage = damage;
        this.description = description;
    }
    public CharacterData() {
        

    }


    public void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public enum Type
    {
        Minion,Hero,Leader,Support
    }
}
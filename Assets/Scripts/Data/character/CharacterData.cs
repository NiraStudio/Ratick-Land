﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData:ScriptableObject
{
    public string characterName;
    public GameObject prefab;
    public Currency buyPrice = new Currency();
    public Currency upgradePrice = new Currency();
    public Type type;
    public int id;
    public int maxLevel;
    public Sprite icon;
    public float speed=3;
    public float attackSpeed;
    public float hitPoint;
    public float attackRange;
    public IntRange damage=new IntRange(0,0);
    public Upgrade upgrade = new Upgrade();
    public string description;

    public CharacterData(string characterName, GameObject prefab, int id, Sprite icon, float speed, float attackSpeed, float hitPoint, IntRange damage, string description)
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
    public CharacterData() { }




    public enum Type
    {
        Minion,Hero,Main,Support
    }
}
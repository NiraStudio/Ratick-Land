using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alpha.Localization;

public class DmgPopUpBehaivior : MonoBehaviour {
    public LocalizedText text;
    public Color EnemyAttackColor, PlayerAttackColor, PlayerHealColor;
    public GameObject a;
    void Start()
    {
        a = Resources.Load("DmgPopUp", typeof(GameObject)) as GameObject;
    }
    public void RePaint(string text,AttackType attackType)
    {
        this.text.After = text;
        switch (attackType)
        {
            case AttackType.playerAttack:
                this.text.t.color = PlayerAttackColor;
                break;
            case AttackType.EnemyAttack:
                this.text.t.color = EnemyAttackColor;
                break;
            case AttackType.PlayerHeal:
                this.text.t.color = PlayerHealColor;
                break;
        }

    }
    public enum AttackType
    {
        playerAttack,EnemyAttack,PlayerHeal
    }
    public void DestroyG()
    {
        Destroy(gameObject);
    }
}



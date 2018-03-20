using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alpha.Localization;

public class DmgPopUpBehaivior : MonoBehaviour {
    public LocalizedDynamicText text;
    public Color EnemyAttackColor, PlayerAttackColor, PlayerHealColor;
    void Start()
    {
    }
    public void RePaint(string text,AttackType attackType,Vector2 position)
    {
        this.text.Number = text;
        switch (attackType)
        {
            case AttackType.playerAttack:
                this.text.Text.color = PlayerAttackColor;
                break;
            case AttackType.EnemyAttack:
                this.text.Text.color = EnemyAttackColor;
                break;
            case AttackType.PlayerHeal:
                this.text.Text.color = PlayerHealColor;
                break;
        }
        gameObject.transform.position = position;
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



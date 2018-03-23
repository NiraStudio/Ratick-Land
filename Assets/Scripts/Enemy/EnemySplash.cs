using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplash : Enemy
{
    Collider2D[] characters;
    public override void Attack()
    {
        
        characters = Physics2D.OverlapCircleAll(centerPoint.transform.position, range, MainBehavior.CharacterLayer);
        foreach (var item in characters)
        {
            item.SendMessage("GetHit", (float)damage);
            Instantiate(DmgPopUp, detectedCharacter.transform.position, Quaternion.identity).GetComponent<DmgPopUpBehaivior>().RePaint(damage.ToString(), DmgPopUpBehaivior.AttackType.EnemyAttack, detectedCharacter.gameObject.transform.position);

        }
        time = 0;
        Attacking = false;
    }
    public void Splash()
    {
        

    }

}

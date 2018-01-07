using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacter : Character
{

   
    public override void Attack()
    {
        detectedEnemy.SendMessage("GetHit", (float)damage);
        print("Attack");

        base.Attack();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


   
}

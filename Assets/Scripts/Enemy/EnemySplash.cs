using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplash : Enemy
{
    Collider2D[] characters;
    public override void Attack()
    {
        anim.SetTrigger("Attack");
        Attacking = true;
    }
    public void Splash()
    {
        characters = Physics2D.OverlapCircleAll(transform.position, range, MainBehavior.CharacterLayer);
        foreach (var item in characters)
        {
            item.SendMessage("GetHit", (float)damage);
        }
        time = 0;
        Attacking = false;

    }

}

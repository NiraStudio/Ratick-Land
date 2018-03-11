using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCharacter : Character {

    public override void Die()
    {
        controller.FinishTheGame();
        base.Die();
    }
    public override void AttackAnimation()
    {
        anim.SetTrigger("Attack");
        base.AttackAnimation();
    }
}

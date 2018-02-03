using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character {

	// Use this for initialization
	
	
	// Update is called once per frame

    public override void Die()
    {
        controller.FinishTheGame();
        base.Die();
    }
    public override void Attack()
    {
        detectedEnemy.SendMessage("GetHit", (float)damage.Random);
        print("Attack");

        base.Attack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCharacter : Character {

    public override void Die()
    {
        base.Die();
        controller.FinishTheGame();
    }
    

    public override void Reset()
    {
        base.Reset();
        gameObject.layer = 13;
    }
}

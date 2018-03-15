using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestCharacter : Character {
    GameObject leader;
    public override void Start()
    {
        base.Start();
        leader = GameObject.FindWithTag("Leader");

    }
    public override void Attack()
    {
        leader.SendMessage("GetHeal", damage.Random);
    }
}

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
        float f = damage.Random;
        leader.SendMessage("GetHeal", f);
        Instantiate(DmgPopUp, null).GetComponent<DmgPopUpBehaivior>().RePaint(((int)f).ToString(),DmgPopUpBehaivior.AttackType.PlayerHeal,leader.transform.position);

        waitTime = 0;
        Attacking = false;
    }
}

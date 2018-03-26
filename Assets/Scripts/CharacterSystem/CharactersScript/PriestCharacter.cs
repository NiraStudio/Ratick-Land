using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestCharacter : Character {
    public GameObject HealPS;
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
        GetComponent<SFX>().PlaySound("Heal");
        //Instantiate(HealPS, leader.transform);
        Attacking = false;
        waitTime = 0;
    }
}

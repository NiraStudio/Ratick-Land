using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy {
    public GameObject bullet,ShootPos;
    public override void Attack()
    {
        if (detectedCharacter != null)
            Instantiate(bullet, ShootPos.transform.position, Quaternion.identity).GetComponent<TargetProjectile>().Spawn(detectedCharacter.gameObject.transform.position, damage);

        Attacking = false;
        time = 0;
    }
	
}

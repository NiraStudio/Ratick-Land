using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy {
    public GameObject bullet;
    public override void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<AimedProjectile>().Spawn(detectedCharacter.gameObject, damage);
        time = 0;
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacter : Character {
    public GameObject Bullet;
    public Transform ShootPos;
    // Use this for initialization
    public override void Attack()
    {
        if (detectedEnemy != null)
        {
            int f = damage.Random;
            Instantiate(Bullet, ShootPos.transform.position, Quaternion.identity).GetComponent<AimedProjectile>().Spawn(detectedEnemy.gameObject, f);

        }
        Attacking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacter : Character, IHitable, IAttackable
{
    public float attackRadius=0.5f;
    float waitTime;
    Collider2D detectedEnemy;
    	
	// Update is called once per frame
	public override void Update () {
        if (!free)
            return;
        waitTime += Time.deltaTime;
        detectedEnemy = Physics2D.OverlapCircle(transform.position, attackRadius, EnemyMask);
        
        if (waitTime > attackSpeed && detectedEnemy)
            Attack();
	}


    public void GetHit(float dmg)
    {
    }

    public void Attack()
    {
        detectedEnemy.SendMessage("GetHit", (float)damage);
        waitTime = 0;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

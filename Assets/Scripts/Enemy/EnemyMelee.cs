using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {

    public override void Start()
    {
        base.Start();
        StartCoroutine(playNormalSound());
    }

    public override void Attack()
    {
        base.Attack();
    }

    IEnumerator playNormalSound()
    {
        yield return new WaitForSeconds(Random.Range(3, 4));
        int a = Random.Range(0, 101);
        if(a<=30)
        {
            GetComponent<SFX>().PlaySound("Sound");
        }
    }
}

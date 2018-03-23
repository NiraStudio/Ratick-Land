using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBoss : Boss {
    public ParticleSystem SplashPS, DirectPs;

    public override void Start()
    {
        base.Start();
        if(PlayerPrefs.GetInt("Tutorial")==1)
        {
            damage *= 1.5f;
            hitPoint *= 2;
        }
    }
    public override void Splash()
    {
        base.Splash();
        SplashPS.gameObject.SetActive(true);
        CameraShake.Instance.Shake(1, 0.4f);

    }
    public override void AttackDirection()
    {
        base.AttackDirection();
        DirectPs.gameObject.SetActive(true);


    }
    public override void Die()
    {
        if (PlayerPrefs.GetInt("FirstBoss") == 1)
        {
            PlayerPrefs.SetInt("BossKilled", 1);
        }
        LevelController.instance.FinishTheGame("Victory");
        GameAnalyticsManager.SendCustomEvent("Boss Killed:BossDefeated");
        base.Die();
    }
}

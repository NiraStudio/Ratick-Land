using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBoss : Boss {
    public ParticleSystem SplashPS, DirectPs;

    public override void Splash()
    {
        base.Splash();
        SplashPS.Emit(30);
        CameraShake.Instance.Shake(1, 0.4f);

    }
    public override void AttackDirection()
    {
        base.AttackDirection();
        DirectPs.Emit(30);

    }
    public override void Die()
    {
        if (PlayerPrefs.GetInt("FirstBoss") == 1)
        {
            PlayerPrefs.SetInt("BossKilled", 1);
        }
        LevelController.instance.FinishTheGame("Victory");
        GameAnalyticsManager.SendCustomEvent("BossDefeated");
        base.Die();
    }
}

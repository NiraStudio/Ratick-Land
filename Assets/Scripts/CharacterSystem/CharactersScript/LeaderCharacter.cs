using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCharacter : Character {

    public override void Start()
    {
        base.Start();
        if (LevelController.instance == null)
            DestroyImmediate(gameObject.GetComponent<AudioListener>());
    }

    public override void Die()
    {
        base.Die();
        LC.FinishTheGame("Defeat");
    }
    

    public override void Reset()
    {
        base.Reset();
        gameObject.layer = 13;
    }
}

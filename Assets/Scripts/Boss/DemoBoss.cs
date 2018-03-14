using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBoss : Boss {

    public override void Die()
    {
        LevelController.instance.FinishTheGame();
        base.Die();
    }
}

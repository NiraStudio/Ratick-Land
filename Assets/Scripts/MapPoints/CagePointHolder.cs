using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagePointHolder : PointHolder {
    public bool created;

    public override void Make()
    {
        CageFinder.Instance.ChangeTarget(Instantiate(Object,transform.position,Quaternion.identity));
        created = true;
    }
}

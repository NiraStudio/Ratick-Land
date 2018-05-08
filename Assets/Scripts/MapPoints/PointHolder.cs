using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHolder : MonoBehaviour {
    public GameObject Object;
    public virtual void Make()
    {
        Instantiate(Object, transform.position, Quaternion.identity);
    }
}

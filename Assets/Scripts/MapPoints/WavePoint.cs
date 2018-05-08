using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePoint : MonoBehaviour {


    public GameObject wave;
    public void Spawn()
    {
        wave = Instantiate(wave, transform.position, Quaternion.identity);
    }

}

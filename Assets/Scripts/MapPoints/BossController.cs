using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    public List<BossPointHolder> BossPoint = new List<BossPointHolder>();
	// Use this for initialization
	void Start () {

        GameObject map = GameObject.FindWithTag("Map");
        for (int i = 0; i < map.transform.childCount; i++)
        {
            if (map.transform.GetChild(i).GetComponent<MapClass>())
                BossPoint.AddRange(map.transform.GetChild(i).GetComponent<MapClass>().freeBossPoints);
        }
        BossPoint[Random.Range(0, BossPoint.Count)].Make() ;
	}
	
	
}

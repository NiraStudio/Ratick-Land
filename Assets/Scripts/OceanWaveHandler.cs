using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanWaveHandler : MonoBehaviour {
    Vector3 center;
    Vector3 MovePos;
    public float speed,MaxY,MaxX;

    int down;
    bool allow;
	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(0.1f);
        center = transform.position;
        down = Random.Range(0, 2);
        StartCoroutine(chooseMovePose());
        allow = true;
    }

    // Update is called once per frame
    void Update () {
        if (!allow)
            return;
		if(transform.position!=MovePos)
        {
            transform.position = Vector3.Lerp(transform.position, MovePos, speed * Time.deltaTime);
        }
	}
    public IEnumerator chooseMovePose()
    {
        MovePos = center;
        if (down == 0)
        {
            MovePos.y -= Random.Range(0, MaxY);
            MovePos.x -= Random.Range(0, MaxX);

            down = 1;
        }else if(down == 1)
        {
            MovePos.y += Random.Range(0, MaxY);
            MovePos.x += Random.Range(0, MaxX);
            down = 0;
        }
        yield return new WaitForSeconds(Random.Range( .5f, 1f));
        StartCoroutine(chooseMovePose());
    }
}

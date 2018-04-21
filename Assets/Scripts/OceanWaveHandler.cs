using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanWaveHandler : MonoBehaviour {
    Vector2 center;
    Vector2 MovePos;
    public float speed,MaxY,MaxX;

    int down;
	// Use this for initialization
	void Start () {
        center = transform.position;
        down = Random.Range(0, 2);
        StartCoroutine(chooseMovePose());
    }

    // Update is called once per frame
    void Update () {
		if((Vector2)transform.position!=MovePos)
        {
            transform.position = Vector2.Lerp(transform.position, MovePos, speed * Time.deltaTime);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageFinder : MonoBehaviour {
    public GameObject cage;

float dis= 5.5f;
    Vector2 t;
    Image img;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!cage)
            return;

        if (Vector2.Distance(Camera.main.transform.position, cage.transform.position) < dis)
            img.enabled = false;
        else
            img.enabled = true;

        t = (cage.transform.position - Camera.main.transform.position).normalized;
        t.x = t.x * 2;
        t.y = t.y * 4.5f;
        transform.position = Camera.main.WorldToScreenPoint((Vector2)Camera.main.transform.position + (t));
        print(transform.position);
    }
    public void addTaget(GameObject t)
    {
        cage = t;
    }
}

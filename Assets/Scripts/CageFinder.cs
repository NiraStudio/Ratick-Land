using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageFinder : MonoBehaviour {
    public GameObject cage;
    public Text meterText;
    public Image img;

    Vector2 t,aa;
    Vector2 distance;


    float xDis = 2, yDis = 4;
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (!cage)
            return;
        distance.x = 3 + (0.5f * (Camera.main.orthographicSize - 5));
        distance.y = 5 +  (Camera.main.orthographicSize - 5);
        t = (cage.transform.position - Camera.main.transform.position) ;

        meterText.text =(int) t.magnitude+" M";

        aa = t;
        aa.x = Mathf.Abs(aa.x);
        aa.y = Mathf.Abs(aa.y);
        if (aa.y < distance.y && aa.x < distance.x)
        {
            img.gameObject.SetActive(false);

            meterText.gameObject.SetActive(false);
        }
        else
        {
            img.gameObject.SetActive(true);

            meterText.gameObject.SetActive(true);

        }


        var angle = Mathf.Atan2(t.y, t.x) * Mathf.Rad2Deg;
        img.gameObject. transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        
        t.Normalize();
        t.x *= xDis;
        t.y *= yDis;



        transform.position = Camera.main.WorldToScreenPoint((Vector2)Camera.main.transform.position + (t));


      // print( Screen.width +" "+transform.position.x);

    }
    public void addTaget(GameObject t)
    {
        cage = t;
    }
    
}

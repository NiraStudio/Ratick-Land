using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageFinder : MonoBehaviour {


    #region Singleton
    public static CageFinder Instance;


    void Awake()
    {
        Instance = this;
    }

    #endregion


    public GameObject cage;
    public Text meterText;
    public Image img;

    Vector2 t,CameraPos;
    Vector2 distance;


    float xDis = 3, yDis = 5;
	// Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (!cage)
            return;


        CameraPos = Camera.main.transform.position;
        if (Vector2.Distance(CameraPos,cage.transform.position)<3)
        {
            img.gameObject.SetActive(false);

            meterText.gameObject.SetActive(false);
        }
        else
        {
            img.gameObject.SetActive(true);

            meterText.gameObject.SetActive(true);
        }

        t = (Vector2) cage.transform.position- CameraPos;
        t.Normalize();
        t.x*= xDis;
        t.y *= yDis;
        transform.position =(Vector2)CameraPos+ t;

        #region PreviousMethod
        /*
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



        transform.position =(Vector2)( Camera.main.WorldToScreenPoint((Vector2)Camera.main.transform.position + (t)));


      // print( Screen.width +" "+transform.position.x);
      */

        #endregion


    }
    public void ChangeTarget(GameObject t)
    {
        cage = t;
    }
    
}

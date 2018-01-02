using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance;
    public Vector3 offSet;
    [SerializeField]
    List<GameObject> Targets;

    public float minZoom = 10f;
    public float maxZoom = 40f;
    public float zoomLimiter = 50f;


    Camera cam;
    Vector3 velocity;
    void Start()
    {
        Targets = new List<GameObject>();
        cam=GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (Targets.Count < 1)
            return;

        Move();
        Zoom();
    }


    void Move()
    {
        transform.position =Vector3.SmoothDamp(transform.position, GetCenterOfTargets() + offSet,ref velocity,0.1f);
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatSize() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,newZoom,Time.deltaTime) ;

    }

    Vector3 GetCenterOfTargets()
    {
        if (Targets.Count == 1)
            return Targets[0].transform.position;


        var bound = new Bounds(Targets[0].transform.position, Vector3.zero);

        foreach (var item in Targets)
        {
            bound.Encapsulate(item.transform.position);
        }
        return bound.center;

    }
    float GetGreatSize()
    {
        

        var bound = new Bounds(Targets[0].transform.position, Vector3.zero);

        foreach (var item in Targets)
        {
            bound.Encapsulate(item.transform.position);
        }
        return bound.size.x;

    }
    public void AddTarget(GameObject target)
    {
        Targets.Add(target);
    }
    public void RemoveTarget(GameObject target)
    {
        Targets.Remove(target);
    }
    
}

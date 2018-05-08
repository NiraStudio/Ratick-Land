using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance;
    public Vector3 offSet;
    [SerializeField]
    List<GameObject> Targets = new List<GameObject>();

    public float minZoom = 10f;
    public float maxZoom = 40f;
    public float zoomLimiter = 50f;


    CinemachineVirtualCamera cam;
    Vector3 velocity;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cam=GetComponent<CinemachineVirtualCamera>();
        offSet.z -= 50;
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
        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, newZoom,Time.deltaTime) ;

    }


    public void ChangeTargets(List<GameObject> characters)
    {
        Targets = characters;
    }

    


    Vector3 GetCenterOfTargets()
    {
        if (Targets.Count == 1)
            return Targets[0].transform.position;
        var bound = new Bounds() ;
        if (Targets[0] != null)
           bound  = new Bounds(Targets[0].transform.position, Vector3.zero);

        foreach (var item in Targets)
        {
            if(item!=null)
            bound.Encapsulate(item.transform.position);
        }
        return bound.center;

    }

    float GetGreatSize()
    {


        var bound = new Bounds(Targets[0].transform.position, Vector3.zero);

        foreach (var item in Targets)
        {
            if (item != null)
            bound.Encapsulate(item.transform.position);
        }
        return bound.size.x;

    }

}

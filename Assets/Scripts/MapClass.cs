using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClass : MonoBehaviour {
    public List<GameObject> WavePoints = new List<GameObject>();
    public List<GameObject> BlockPoints= new List<GameObject>();
    public List<GameObject> freeBossPoints = new List<GameObject>();
    public List<GameObject> CagePoints = new List<GameObject>();
    public GameObject startPoint;
    public GameObject[] blocks;
    public Sprite bg;
    void Start()
    {
        GameObject g = new GameObject();
    }

    public List<Vector2> points(PointsType pointType)
    {
        List<Vector2> t = new List<Vector2>();
        switch (pointType)
        {
            case PointsType.Wave:
                foreach (var item in WavePoints.ToArray())
                {
                    t.Add(item.transform.position);
                }
                break;
            case PointsType.Boss:
                foreach (var item in freeBossPoints.ToArray())
                {
                    t.Add(item.transform.position);
                }
                break;
            case PointsType.Block:
                foreach (var item in CagePoints.ToArray())
                {
                    t.Add(item.transform.position);
                }
                break;
            case PointsType.Cage:
                foreach (var item in blocks)
                {
                    t.Add(item.transform.position);
                }
                break;

        }
        return t;
    }
    public Vector2 StartPoint
    {
        get
        {
            return startPoint.transform.position; 
        }
    }
    public void DestroyGameObjects()
    {
        foreach (var item in WavePoints.ToArray())
        {
            Destroy(item);
        }
        foreach (var item in freeBossPoints.ToArray())
        {
            Destroy(item);
        }
        foreach (var item in CagePoints.ToArray())
        {
            Destroy(item);
        }
        foreach (var item in BlockPoints.ToArray())
        {
            Destroy(item);
        }
        Destroy(startPoint);

    }
    
}
public enum PointsType
{
    Wave,Boss,Block,Start,Cage
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClass : MonoBehaviour {
    public List<GameObject> FreePoints = new List<GameObject>();
    public List<GameObject> freeBossPoints = new List<GameObject>();
    public List<GameObject> BlockPoints = new List<GameObject>();
    public GameObject startPoint;
    public GameObject blocks;
    public Sprite bg;
    void Start()
    {
        GameObject g = new GameObject();
        g.AddComponent<SpriteRenderer>().sprite = bg;
        g.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    public List<Vector2> points(PointsType pointType)
    {
        List<Vector2> t = new List<Vector2>();
        switch (pointType)
        {
            case PointsType.Free:
                foreach (var item in FreePoints.ToArray())
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
                foreach (var item in BlockPoints.ToArray())
                {
                    t.Add(item.transform.position);
                }
                break;
            
        }
        return t;
    }
    public Vector2 StartPoint()
    {
        return startPoint.transform.position;
    }
    public void DestroyGameObjects()
    {
        foreach (var item in FreePoints.ToArray())
        {
            Destroy(item);
        }
        foreach (var item in freeBossPoints.ToArray())
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
    Free,Boss,Block,Start
}

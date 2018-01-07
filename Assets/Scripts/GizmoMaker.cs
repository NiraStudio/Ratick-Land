using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoMaker : MonoBehaviour {
    public Transform Center;
    public Color lineColor = Color.white, CircleColor = Color.white, BoxColor = Color.white, CubeColor = Color.white, SphereColor = Color.white;
    public bool Line, Circle, Box, Cube, Sphere;
    public float radius;
    public Vector2 BoxSize;
    public Vector3 CubeSize;
    public float distance;
    public Vector2 Direction;
    public GameObject Aim;
    public LineStyle lineStyle;
	// Use this for initialization
	
    void OnDrawGizmos()
    {
        if (Line)
        {
            Gizmos.color = lineColor;
            
            switch (lineStyle)
            {
                case LineStyle.Aimer:
                    Gizmos.DrawLine(Center.position, Aim.transform.position);
                    break;
                case LineStyle.Direction:
                    Gizmos.DrawLine(Center.position, Direction * distance);
                    break;
            }

        }
        if(Sphere){
            Gizmos.color = SphereColor;
            
            Gizmos.DrawSphere(Center.position, radius);
        }
        if(Cube){
            Gizmos.color = CubeColor;

            Gizmos.DrawCube(Center.position, CubeSize);
        }
        if(Box){
            Gizmos.color = BoxColor;
            Gizmos.DrawWireCube(Center.position, (Vector3)BoxSize);
        }
        if(Circle){
            Gizmos.color = CircleColor;

            Gizmos.DrawWireSphere(Center.position, radius);
        }
    }
    public enum LineStyle
    {
        Aimer, Direction
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GizmoMaker))]
[ExecuteInEditMode]

public class GizmoMakerEditor : Editor {
    
    
    public override void OnInspectorGUI()
    {
        GizmoMaker gizmo =(GizmoMaker) target;

        gizmo.Center = EditorGUILayout.ObjectField("Target : ", gizmo.Center, typeof(Transform)) as Transform;
       

         gizmo.Line = EditorGUILayout.ToggleLeft("Line",gizmo.Line);


        gizmo.Box = EditorGUILayout.ToggleLeft("Box",gizmo.Box);

        gizmo.Circle = EditorGUILayout.ToggleLeft("Circle",gizmo.Circle);

        gizmo.Sphere = EditorGUILayout.ToggleLeft("Sphere",gizmo.Sphere);

        gizmo.Cube = EditorGUILayout.ToggleLeft("Cube",gizmo.Cube);
     

        if (gizmo.Box)
        {
            gizmo.Cube = false;
            gizmo.BoxSize = EditorGUILayout.Vector2Field("Size Of Box Gizmo :", gizmo.BoxSize);
            gizmo.BoxColor = EditorGUILayout.ColorField(gizmo.BoxColor);

        }
        if (gizmo.Line)
        {
            gizmo.lineColor = EditorGUILayout.ColorField(gizmo.lineColor);
            gizmo.distance = EditorGUILayout.FloatField("Line Lenght :", gizmo.distance);
            gizmo.lineStyle =(GizmoMaker.LineStyle) EditorGUILayout.EnumPopup("Line Type",gizmo.lineStyle);
            switch (gizmo.lineStyle)
            {
                case GizmoMaker.LineStyle.Aimer:
                    gizmo.Aim = EditorGUILayout.ObjectField("Target : ", gizmo.Aim, typeof(GameObject)) as GameObject;
                    break;
                case GizmoMaker.LineStyle.Direction:
                    gizmo.Direction = EditorGUILayout.Vector3Field("Line Diretion :", gizmo.Direction);
                    break;
                
            }


        }
        if (gizmo.Sphere)
        {
            gizmo.Circle = false;
            gizmo.radius=EditorGUILayout.FloatField("Radius :",gizmo.radius);
            gizmo.SphereColor = EditorGUILayout.ColorField(gizmo.SphereColor);

        }
        if (gizmo.Circle)
        {
            gizmo.Sphere = false;
            gizmo.radius=EditorGUILayout.FloatField("Radius :",gizmo.radius);
            gizmo.CircleColor = EditorGUILayout.ColorField(gizmo.CircleColor);
            
        }
        if (gizmo.Cube)
        {
            gizmo.Box = false;
            gizmo.CubeSize = EditorGUILayout.Vector3Field("Size Of Cube Gizmo :", gizmo.CubeSize);
            gizmo.CubeColor = EditorGUILayout.ColorField(gizmo.CubeColor);
        }
    }
	
}

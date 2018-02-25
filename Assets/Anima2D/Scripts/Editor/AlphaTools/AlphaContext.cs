using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Anima2D;

public class AlphaContext
{
    // Use this for initialization

    [MenuItem("Assets/Create/Anima2D/Alpha/Make Sprite Meshs")]
    public static void CreateSpriteMesh()
    {
        object[] aa= Selection.objects;
        foreach (object Item in aa)
        {
            SpriteMeshUtils.CreateSpriteMesh(Item as Sprite);
            Debug.Log(Item.GetType());
        }


    }
}

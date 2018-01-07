using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour {
    public int MoveDistance,MoveTimes,tileLenghtIncrease;
    List<Vector2> FieldPoses = new List<Vector2>();
    public List<Vector2> FreeSpots = new List<Vector2>();
    public Vector2 maxSize;
    public GameObject pathMaker,wallsParent;
    public Tilemap fieldTile;
    public TileBase fieldShape;
    public Vector2 highestPoint,lowestPoint;
    public GameObject wallShape;
    Direction direc;
    int currentFieldTiles;
    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }
	// Use this for initialization
	
	// Update is called once per frame
    public List<Vector2> makeMap()
    {
        SetUpField();
        SetUpWalls();

        return FreeSpots;
    }
    void SetUpField()
    {
        //making fist Tile
        for (int x = (int)pathMaker.transform.position.x - (tileLenghtIncrease + 3); x < pathMaker.transform.position.x + tileLenghtIncrease + 1; x++)
        {
            for (int y = (int)pathMaker.transform.position.y - (tileLenghtIncrease + 2); y < pathMaker.transform.position.y + tileLenghtIncrease + 1; y++)
            {

                Vector2 tt = new Vector2(x, y);
                if (tt.x > highestPoint.x)
                    highestPoint.x = tt.x;

                if (tt.y > highestPoint.y)
                    highestPoint.y = tt.y;


                if (tt.x < lowestPoint.x)
                    lowestPoint.x = tt.x;


                if (tt.y < lowestPoint.y)
                    lowestPoint.y = tt.y;


                FieldPoses.Add(tt);
                fieldTile.SetTile(Vector3Int.RoundToInt(tt), fieldShape);

            }
        }
        highestPoint = lowestPoint = transform.position;


        //RoadMaking
        while (currentFieldTiles<MoveTimes)
        {


            Direction d;
            do
            {
                d = (Direction)Random.Range(0, 4);
            } while ((int)d == ((int)direc + 2) % 4);
            Vector2 t = new Vector2();
            switch (d)
            {
                case Direction.Up:
                    t = Vector2.up;

                    break;
                case Direction.Left:
                    t = Vector2.left;
                    break;
                case Direction.Down:
                    t = Vector2.down;
                    break;
                case Direction.Right:
                    t = Vector2.right;
                    break;

            }

            pathMaker.transform.position = (Vector2)pathMaker.transform.position + (t * MoveDistance);
            if (pathMaker.transform.position.x < -(maxSize.x / 2) || pathMaker.transform.position.x > (maxSize.x / 2) || pathMaker.transform.position.y < -(maxSize.y / 2) || pathMaker.transform.position.y > (maxSize.y / 2))
            {
                continue;
            }

            if (!FieldPoses.Contains(pathMaker.transform.position))
            {
                FreeSpots.Add(pathMaker.transform.position);
                for (int x = (int)pathMaker.transform.position.x - tileLenghtIncrease; x < pathMaker.transform.position.x + tileLenghtIncrease; x++)
                {
                    for (int y = (int)pathMaker.transform.position.y - tileLenghtIncrease; y < pathMaker.transform.position.y + tileLenghtIncrease; y++)
                    {

                        Vector2 tt = new Vector2(x, y);
                        if (tt.x > highestPoint.x)
                            highestPoint.x = tt.x;

                        if (tt.y > highestPoint.y)
                            highestPoint.y = tt.y;


                        if (tt.x < lowestPoint.x)
                            lowestPoint.x = tt.x;


                        if (tt.y < lowestPoint.y)
                            lowestPoint.y = tt.y;


                        fieldTile.SetTile(Vector3Int.RoundToInt(tt), fieldShape);
                        if (!FieldPoses.Contains(tt))
                        {
                            FieldPoses.Add(tt);
                            currentFieldTiles++;
                        }

                    }
                }

            }



            direc = d;

        }

    }
    void SetUpWalls()
    {
         Vector2 down= lowestPoint - (Vector2.one * 10);
        Vector2 up = highestPoint + (Vector2.one * 10);
        for (int i = (int)down.x; i < up.x; i++)
        {
            for (int j = (int)down.y; j < up.y; j++)
            {
                Vector2 t = new Vector2(i, j);
                if(!FieldPoses.Contains(t))
                {
                    //wallTile.SetTile(Vector3Int.RoundToInt(t), wallShape);
                    t += Vector2.one * 0.5f;
                    GameObject g = Instantiate(wallShape, t, Quaternion.identity);
                    g.transform.SetParent(wallsParent.transform);
                }
            }
        }
    }
    void RespawnObjects()
    {
        for (int i = 0; i < 10; i++)
        {
            //Instantiate(enemy, FreeSpots[Random.Range(0, FreeSpots.Count)], Quaternion.identity);            
        }
    }
}

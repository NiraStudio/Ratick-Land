using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    public float MinSpeed=350;
    public float MinXDistance=0.3f;
    public float MinYDistance=0.3f;
    public UnityEvent Up, Down, Left, Right;

    Vector2 BeginPos,currentPos;
    bool touch;
    public void FixedUpdate()
    {
        if (Input.touches.Length > 0 )
        {
            Touch t = Input.GetTouch(0);
            if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(t.fingerId))
            switch (t.phase)
            {
                case TouchPhase.Began:
                    BeginPos=Camera.main.ScreenToWorldPoint(t.position);
                    touch = true;
                    break;


                case TouchPhase.Moved:
                    //print(Mathf.Abs(BeginPos.y - t.position.y));
                   // print(Mathf.Abs(BeginPos.x - t.position.x));

                    if (!touch)
                        break;

                    if(t.deltaPosition.magnitude/t.deltaTime>=MinSpeed)
                    {
                        currentPos = Camera.main.ScreenToWorldPoint(t.position);


                        //X
                        if (Mathf.Abs(BeginPos.x - currentPos.x) >= MinXDistance)
                        {
                            if (currentPos.x > BeginPos.x)
                            {
                                //swap Right
                                print("Right" + "Speed = " + t.deltaPosition.magnitude / t.deltaTime + " Dis = " + Mathf.Abs(BeginPos.x - currentPos.x));
                                touch = false;
                                Right.Invoke();
                            }
                            else if (currentPos.x < BeginPos.x)
                            {
                                //swap Left
                                print("Left" + "Speed = " + t.deltaPosition.magnitude / t.deltaTime + " Dis = " + Mathf.Abs(BeginPos.x - currentPos.x));
                                touch = false;
                                Left.Invoke();
                            }

                        }
                        //Y
                        else if (Mathf.Abs(BeginPos.y - currentPos.y) >= MinYDistance)
                        {
                            if (currentPos.y > BeginPos.y)
                            {
                                //swap Up
                                print("Up" + "Speed = " + t.deltaPosition.magnitude / t.deltaTime + " Dis = " + Mathf.Abs(BeginPos.y - currentPos.y));
                                touch = false;
                                Up.Invoke();
                            }
                            else if (currentPos.y < BeginPos.y)
                            {
                                //swap Down
                                print("Down" + "Speed = " + t.deltaPosition.magnitude / t.deltaTime + " Dis = " + Mathf.Abs(BeginPos.y - currentPos.y));
                                touch = false;
                                Down.Invoke();
                            }
                        }

                    }
                    break;


                case TouchPhase.Ended:
                    touch = false;
                    break;
            }
        }
    }
}

   
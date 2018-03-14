using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class NonUiTouch : MonoBehaviour
{
    public UnityEvent OnTouch;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            if(!EventSystem.current.IsPointerOverGameObject()&& hitInfo)
            if (hitInfo.collider.gameObject==gameObject)
            {
                OnTouch.Invoke();
            }
        }



#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && hitInfo)
                    if (hitInfo.collider.gameObject == gameObject)
                    {
                        OnTouch.Invoke();
                    }
            }
        }
#endif

    }


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public Transform target;
    public CameraPos CurrentState;
    public Pos[] poses;
    [Range(0, 500)]
    public float smoothness;
    public bool Allow=true;
    // Use this for initialization
    void Start()
    {
        ChangeByCurrentState();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeView(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ChangeView(1);
        
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 a = target.position;
        a.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, a, (smoothness / 100) * Time.deltaTime);
    }
    public void ChangeView(int state)
    {
        if (!Allow)
            return;

        if (state < 0 && CurrentState == 0)
            return;
        if (state > 0 && CurrentState == (CameraPos)(System.Enum.GetValues(typeof(CameraPos)).Length - 1))
            return;

        if (CurrentState + state != CameraPos.Sky)
            CurrentState = CurrentState + state;

        ChangeByCurrentState();
    }

    public void ChangeByCurrentState()
    {
        foreach (var item in poses)
        {
            if (item.PosName == CurrentState)
            {
                target = item.position;
                break;
            }
        }
    }
    public void SendToSky()
    {
        CurrentState = CameraPos.Sky;
        ChangeByCurrentState();

    }
    //views by order
    public enum CameraPos
    {
        Shop,Campaign, Main, CommingSoon,Sky
    }
    [System.Serializable]
    public class Pos
    {
        public CameraPos PosName;
        public Transform position;
    }
}

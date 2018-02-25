using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public Transform target;
    public CameraPos CurrentState=0;
    public Pos[] poses;
    [Range(0, 500)]
    public float smoothness;
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
        if (state < 0 && CurrentState == CameraPos.Campaign)
            return;
        if (state > 0 && CurrentState == (CameraPos)(System.Enum.GetValues(typeof(CameraPos)).Length - 1))
            return;
        CurrentState = (CameraPos)(CurrentState + state);
        ChangeByCurrentState();
    }

    void ChangeByCurrentState()
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

    //views by order
    public enum CameraPos
    {
        Campaign, Main, CommingSoon
    }
    [System.Serializable]
    public class Pos
    {
        public CameraPos PosName;
        public Transform position;
    }
}

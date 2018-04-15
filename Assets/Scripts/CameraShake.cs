using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public static CameraShake Instance;

    public float Duration;
    public float Power;

    bool allow;
    float t;
    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        t = Duration;
	}
	
	// Update is called once per frame
	void Update () {
        if (Duration > 0&&allow)
        {
            Vector2 r = Random.insideUnitCircle * (Power/10);
            transform.position = new Vector3(transform.position.x + r.x, transform.position.y + r.y, transform.position.z);
            Duration -= Time.deltaTime;
            if (Duration <= 0)
                allow = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
	}

    public void Shake(float power, float duration)
    {
        Duration =t= duration;
        Power = power;
        allow = true;
        Handheld.Vibrate();

    }

    public void Shake()
    {
        Duration = t;
        allow = true;
        Handheld.Vibrate();
    }
}

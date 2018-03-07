using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsManager : MonoBehaviour {
    #region Singleton
    public static GameAnalyticsManager Instance;
    void Awake()
    {

    }
    #endregion
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void SendCustomEvent(string EventName,float EventValue)
    {
        GameAnalytics.NewDesignEvent(EventName, EventValue);
    }
    public static void SendCustomEvent(string EventName)
    {
        GameAnalytics.NewDesignEvent(EventName);
    }
}

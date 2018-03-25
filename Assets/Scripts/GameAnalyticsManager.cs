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

    InternetChecker IC;
    // Use this for initialization
    void Start () {
        IC = GetComponent<InternetChecker>();
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
        GameAnalytics.NewDesignEvent(EventName,1);
    }
}
public class DesignEventInfo
{
    public string Id;
    public float Value;
}
public class ProgressionEventInfo
{
    public GAProgressionStatus Status;
    public string Progress1,Progress2,Progress3;
    public int score;
}

public class ResourcesEventInfo
{

}


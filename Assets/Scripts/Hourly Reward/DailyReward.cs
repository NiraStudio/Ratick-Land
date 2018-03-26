using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class DailyReward : MonoBehaviour
{
    //UI
    public LocalizedDynamicText timeLabel; //only use if your timer uses a label
    public Button timerButton; //used to disable button when needed
    public Image _progress,OutOfNet;
    //TIME ELEMENTS
    public int hours; //to set the hours
    public int minutes; //to set the minutes
    public int seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private TimeSpan _remainingTime;
    double coolDown;
    //progress filler
    private float _value = 1f;
    //reward to claim
    public int RewardToEarn;



    //startup
    IEnumerator Start()
    {
        OutOfNet.gameObject.SetActive(true);
        yield return new WaitUntil(() => InternetChecker.Instance.internetConnectBool);
        OutOfNet.gameObject.SetActive(false);

        if (PlayerPrefs.GetString("_timer") == "")
        {
            Debug.Log("==> Enableing button");
            enableButton();
        }
        else
        {
            disableButton();
            StartCoroutine("CheckTime");
        }
    }



    //update the time information with what we got some the internet
    private void updateTime()
    {
        if (PlayerPrefs.GetString("_timer") == "Standby")
        {
            PlayerPrefs.SetString("_timer", TimeManager.sharedInstance.getCurrentTimeNow());
            PlayerPrefs.SetInt("_date", TimeManager.sharedInstance.getCurrentDateNow());
        }
        else if (PlayerPrefs.GetString("_timer") != "" && PlayerPrefs.GetString("_timer") != "Standby")
        {
            int _old = PlayerPrefs.GetInt("_date");
            int _now = TimeManager.sharedInstance.getCurrentDateNow();


            //check if a day as passed
            if (_now > _old)
            {//day as passed
                Debug.Log("Day has passed");
                enableButton();
                return;
            }
            else if (_now == _old)
            {//same day
                Debug.Log("Same Day - configuring now");
                _configTimerSettings();
                return;
            }
            else
            {
                Debug.Log("error with date");
                return;
            }
        }
        Debug.Log("Day had passed - configuring now");
        _configTimerSettings();
    }

    //setting up and configureing the values
    //update the time information with what we got some the internet
    private void _configTimerSettings()
    {
        _startTime = TimeSpan.Parse(PlayerPrefs.GetString("_timer"));
        _endTime = TimeSpan.Parse(hours + ":" + minutes + ":" + seconds);
        TimeSpan temp = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
        TimeSpan diff = temp.Subtract(_startTime);
        _remainingTime = _endTime.Subtract(diff);
        coolDown = _remainingTime.TotalSeconds;
        //start timmer where we left off
        setProgressWhereWeLeftOff();

        if (diff >= _endTime)
        {
            _timerComplete = true;
            enableButton();
        }
        else
        {
            _timerComplete = false;
            disableButton();
            _timerIsReady = true;
        }
    }

    //initializing the value of the timer
    private void setProgressWhereWeLeftOff()
    {
        float ah = 1f / (float)_endTime.TotalSeconds;
        float bh = 1f / (float)_remainingTime.TotalSeconds;
        _value = ah / bh;
        _progress.fillAmount = _value;
    }



    //enable button function
    private void enableButton()
    {
        timerButton.interactable = true;
    }



    //disable button function
    private void disableButton()
    {
        timerButton.interactable = false;
    }


    //use to check the current time before completely any task. use this to validate
    private IEnumerator CheckTime()
    {
        disableButton();
        Debug.Log("==> Checking for new time");
        yield return StartCoroutine(
            TimeManager.sharedInstance.getTime()
        );
        updateTime();
        Debug.Log("==> Time check complete!");

    }


    //trggered on button click
    public void rewardClicked()
    {
        Debug.Log("==> Claim Button Clicked");
        PlayerPrefs.SetString("_timer", "Standby");
        StartCoroutine("CheckTime");
    }



    //update method to make the progress tick
    void Update()
    {
        if (InternetChecker.Instance.internetConnectBool)
        {
            if (_timerIsReady)
            {
                if (!_timerComplete && PlayerPrefs.GetString("_timer") != "")
                {
                    OutOfNet.gameObject.SetActive(false);

                    _value -= Time.deltaTime * 1f / (float)_endTime.TotalSeconds;
                    _progress.fillAmount = _value;
                    coolDown -= Time.deltaTime;
                    double tt = coolDown;
                    string r = "";
                    r += ((int)tt / 3600).ToString() + ":";
                    tt -= ((int)tt / 3600) * 3600;
                    r += ((int)tt / 60).ToString("00") + ":";
                    r += (tt % 60).ToString("00");
                    timeLabel.Number = r;
                    //this is called once only
                    if (_value <= 0 && !_timerComplete)
                    {
                        //when the timer hits 0, let do a quick validation to make sure no speed hacks.
                        validateTime();
                        _timerComplete = true;
                    }
                }
            }
        }
        else
        {
            OutOfNet.gameObject.SetActive(true);
            timeLabel.Number = "";
        }
    }



    //validator
    private void validateTime()
    {
        Debug.Log("==> Validating time to make sure no speed hack!");
        StartCoroutine("CheckTime");
    }


    

}
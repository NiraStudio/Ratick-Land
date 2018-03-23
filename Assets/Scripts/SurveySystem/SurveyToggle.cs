using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyToggle : MonoBehaviour {

    public SurveyManager manager;
    public string Id;
    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(Changed);
    }
    public void Changed(bool Select)
    {
        if (Select)
        {
            manager.data.Add(Id);
        }
        else
        {
            manager.data.Remove(Id);

        }

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentManager : MonoBehaviour {
    
    public AchievmentDataBase dB;
    public int current;
    
    
    public void Restart()
    {
        foreach(Achievment achiv in dB.dB)
        {
            if (achiv.resetAble)
            {
                achiv.Reset();
            }
        }    
    }
    public void Increase(int current)
    {
        
    }
    void Start () {
		
	}
	
	void Update () {
		
	}
}

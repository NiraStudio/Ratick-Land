using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour {
    public BossAction[] Actions;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChooseAction()
    {
        float dice;
        float temp;
        for (int j = 0; j < Actions.Length; j++)
        {
            dice = Random.Range(0, 101);
            temp = 0;
            for (int i = 0; i < Actions.Length; i++)
            {
                temp += Actions[i].chance;
                if (dice < temp)
                {
                    Actions[i].Action.Invoke();
                    break;
                }
            }
        }
    }
    
}
[System.Serializable]
public class BossAction
{
    public UnityEvent Action;
    public int chance;
}

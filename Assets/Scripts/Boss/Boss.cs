using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour {
    public BossAction[] Actions;
	// Use this for initialization
	void Start () {
        //Sorting The Actions In Order
        Sorting();


	}
	
    
	// Update is called once per frame
	void Update () {
        
	}

    void Sorting()
    {
        BossAction aa = new BossAction();
        for (int i = 0; i < Actions.Length - 1; i++)
            for (int j = 0; j < Actions.Length - 1; j++)
                if (Actions[j].chance > Actions[j + 1].chance)
                {
                    aa = Actions[j];
                    Actions[j] = Actions[j + 1];
                    Actions[j + 1] = aa;
                }
    }
    public void ChooseAction()
    {
        float dice;
        float temp;

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
[System.Serializable]
public class BossAction
{
    public UnityEvent Action;
    public int chance;
}

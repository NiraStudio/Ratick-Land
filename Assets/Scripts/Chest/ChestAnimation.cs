using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimation : MonoBehaviour {
    public GameObject CardHolder;
    public GameObject CardUI;
    int a ;
    int index=0;
    public Animator CardHolderAnim,chestAnim;
    bool open,allow;
	// Use this for initialization
	void Start () {
       
        CardHolder.gameObject.SetActive(true);
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void buttonDown()
    {
        
        if (open)
        {
            if (allow)
            {
                if (index < a)
                {
                    ChangeCard();

                }
                else
                {
                    CardHolder.gameObject.SetActive(false);
                    open = false;
                    for (int i = 0; i < CardHolder.transform.childCount; i++)
                    {
                        Destroy(CardHolder.transform.GetChild(i).gameObject);
                    }
                    allow = false;
                    gameObject.SetActive(false);
                    ChestManager.Instance.Close();
                }
            }
        }
        else
        {
            chestAnim.SetTrigger("Open");

            CardHolder.gameObject.SetActive(true);
            open = true;
        }
        

    }
    public void ChangeCard()
    {
        for (int i = 0; i < CardHolder.transform.childCount; i++)
        {
            CardHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
        print(a + " " + index);
        CardHolder.transform.GetChild(index).gameObject.SetActive(true);
        index++;
        CardHolderAnim.SetTrigger("Open");
    }
    public void Open(List<RewardInfo> rewards,RewardInfo Ad)
    {
        RewardCard c = new RewardCard();
        
        foreach (var item in rewards.ToArray())
        {
            c = Instantiate(CardUI, CardHolder.transform).GetComponent<RewardCard>();
            c.Repaint(item);
            c.gameObject.SetActive(false);
        }
        if(Ad!=null)
        {
            c = Instantiate(CardUI, CardHolder.transform).GetComponent<RewardCard>();
            c.Repaint(Ad);
            c.gameObject.SetActive(false);
        }
        index = 0;
        a = CardHolder.transform.childCount;
        gameObject.SetActive(true);
    }
    public void Allower()
    {
        allow = true;
        print("sss");
    }

}

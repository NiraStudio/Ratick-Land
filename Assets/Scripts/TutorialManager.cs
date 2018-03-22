using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Alpha.Localization;

public class TutorialManager : MonoBehaviour {
    public static TutorialManager Instance;

    public TutorialSteps[] steps = new TutorialSteps[0];
    public Button ClickPanel;
    public GameObject GirlLook;
    public GameObject[] Pointers;
    public Animator anim;
    public LocalizedKeyText text;

    GameManager GM;
	// Use this for initialization
	void Awake () {
        Instance = this;
        this.gameObject.SetActive(false);
	}
	void Start()
    {
        DeActivePointers();
        GM = GameManager.instance;
    }
	// Update is called once per frame

    public void OpenStep(string Id)
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        foreach (var item in steps)
        {
            if (item.TextKey == Id)
            {
                ClickPanel.onClick.RemoveAllListeners();
                if (item.Animation)
                    anim.SetTrigger("Talk");
                GirlLook.SetActive(item.Look);
                text.Key = item.TextKey;
                ClickPanel.onClick.AddListener( item.NextStep.Invoke);
            }
        }
    }
    public void GiveCharacters()
    {
        GM.IncreaseCharacterLevel(2, 25);
        GM.AddCharacter(3, 1, 0);
        GM.AddCharacter(4, 1, 0);
        GM.AddCharacter(5, 1, 0);

        ArrangeSceneManager.Instance.MinionChange(2);
        ArrangeSceneManager.Instance.SuppChange(5);
        ArrangeSceneManager.Instance.HeroChange(3,0);
        ArrangeSceneManager.Instance.HeroChange(4,2);
    }
    public void GameObjectState(bool state)
    {
        gameObject.SetActive(state);
    }
    public void GoToArrangeScene()
    {
        MainMenuManager.Instance.Play();
    }
    public void Test()
    {
        print("Working");
    }
    public void DeActivePointers()
    {
        for (int i = 0; i < Pointers.Length; i++)
        {
            Pointers[i].SetActive(false);
        }
    }
    public void OpenPointer(int Index)
    {
        Pointers[Index].SetActive(true);
    }
}
[System.Serializable]
public class TutorialSteps
{
    public string TextKey;
    public bool Animation;
    public bool Look;
    public UnityEvent NextStep;
}

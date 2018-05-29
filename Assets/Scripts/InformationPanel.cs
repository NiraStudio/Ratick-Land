using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Alpha.Localization;
public class InformationPanel : MonoBehaviour {
    #region SingleTon
    public static InformationPanel Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public InformationByType[] Panels;
    public Animator PanelAnimator;
    public GameObject BackPanel;
    public AdScript AdPanelScript;
    public Color SuccusColor, AlertColor, NormalColor;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0f);
        PanelAnimator.gameObject.SetActive(false);
        BackPanel.SetActive(false);
    }
    public void OpenInfoPanel(string Persian,string English, PanelColor color, bool changeNumbers, UnityAction OkAction)
    {
        DeactiveAll();
        foreach (var item in Panels)
        {
            if (item.Type == InformationType.Info)
            {
                item.Panel.SetActive(true);
                switch (color)
                {
                    case PanelColor.Normal:
                        item.Panel.GetComponent<Image>().color = NormalColor;
                        break;
                    case PanelColor.Alert:
                        item.Panel.GetComponent<Image>().color = AlertColor;

                        break;
                    case PanelColor.Succuss:
                        item.Panel.GetComponent<Image>().color = SuccusColor;

                        break;

                }
                item.text.Text(Persian, English);
                item.buttons[0].onClick.RemoveAllListeners();
                if(OkAction!=null)
                item.buttons[0].onClick.AddListener(OkAction);
                item.buttons[0].onClick.AddListener(AnimationClose);
            }
        }
        PanelAnimator.gameObject.SetActive(true);
        PanelAnimator.SetTrigger("Open");
        BackPanel.SetActive(true);


    }

   
    public void OpenRewardPanel(PanelColor color,RewardInfo Info, UnityAction OkAction,string OkButtonText)
    {
        DeactiveAll();
        foreach (var item in Panels)
        {
            if (item.Type == InformationType.Reward)
            {
                item.Panel.SetActive(true);
                switch (color)
                {
                    case PanelColor.Normal:
                        item.Panel.GetComponent<Image>().color = NormalColor;
                        break;
                    case PanelColor.Alert:
                        item.Panel.GetComponent<Image>().color = AlertColor;

                        break;
                    case PanelColor.Succuss:
                        item.Panel.GetComponent<Image>().color = SuccusColor;

                        break;
                   
                }
                item.Panel.transform.GetChild(0).GetComponent<RewardCard>().Repaint(Info);
                item.buttons[0].onClick.RemoveAllListeners();
                if (OkAction != null)
                    item.buttons[0].onClick.AddListener(OkAction);
                item.buttons[0].onClick.AddListener(AnimationClose);
                item.buttons[0].transform.GetChild(0).GetComponent<Text>().text = OkButtonText;
            }
        }
        PanelAnimator.gameObject.SetActive(true);
        PanelAnimator.SetTrigger("Open");
        BackPanel.SetActive(true);

    }

    public void OpenFinshPanel(string StateKey,PanelColor color,int CointAmount ,UnityAction OkAction)
    {
        DeactiveAll();
        LocalizedDynamicText t=new LocalizedDynamicText();
        foreach (var item in Panels)
        {
            if (item.Type == InformationType.FinishPanel)
            {
                item.Panel.SetActive(true);
                item.buttons[0].onClick.RemoveAllListeners();
                if (OkAction != null)
                    item.buttons[0].onClick.AddListener(OkAction);


                switch (color)
                {
                    case PanelColor.Normal:
                        item.Panel.GetComponent<Image>().color = NormalColor;
                        break;
                    case PanelColor.Alert:
                        item.Panel.GetComponent<Image>().color = AlertColor;

                        break;
                    case PanelColor.Succuss:
                        item.Panel.GetComponent<Image>().color = SuccusColor;

                        break;

                }

                item.Panel.transform.GetChild(0).GetComponent<Image>().color = item.Panel.transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
                item.Panel.transform.GetChild(2).GetChild(0).GetComponent<LocalizedKeyText>().Key = StateKey;
                t = item.text;
                t.Text("0","0");
                item.buttons[0].onClick.AddListener(AnimationClose);
            }
        }
        PanelAnimator.gameObject.SetActive(true);
        PanelAnimator.SetTrigger("Open");
        BackPanel.SetActive(true);
        if (OkAction != null)
        {
            BackPanel.GetComponent<Button>().onClick.AddListener(OkAction);
            BackPanel.GetComponent<Button>().onClick.AddListener(RestartBackPanel);
        }
        StartCoroutine(changeCoinText(t, CointAmount));
    }
    void RestartBackPanel()
    {
        BackPanel.GetComponent<Button>().onClick.RemoveAllListeners();
        BackPanel.GetComponent<Button>().onClick.AddListener(AnimationClose);

    }
    IEnumerator changeCoinText(LocalizedDynamicText text, int amount)
    {
        yield return new WaitForSeconds(0.7f);
        float coinTemp = 0;
        float lerp = 0;
        while (coinTemp < amount)
        {
            yield return null;
            lerp += Time.deltaTime / 1;
            coinTemp = Mathf.Lerp(coinTemp, amount, lerp);


            text.text = ((int)coinTemp).ToString();
        }
    }
    public void OpenADRewardPanel(string PrText,string EnText,PanelColor color)
    {
        DeactiveAll();
        foreach (var item in Panels)
        {
            if (item.Type == InformationType.AdReward)
            {
                item.Panel.SetActive(true);
                switch (color)
                {
                    case PanelColor.Normal:
                        item.Panel.GetComponent<Image>().color = NormalColor;
                        break;
                    case PanelColor.Alert:
                        item.Panel.GetComponent<Image>().color = AlertColor;

                        break;
                    case PanelColor.Succuss:
                        item.Panel.GetComponent<Image>().color = SuccusColor;

                        break;

                }
                item.text.Text(PrText, EnText);
            }
        }
        PanelAnimator.gameObject.SetActive(true);
        PanelAnimator.SetTrigger("Open");
        BackPanel.SetActive(true);

    }
    public void Close()
    {
        PanelAnimator.gameObject.SetActive(false);
        BackPanel.SetActive(false);

    }
    public void AnimationClose()
    {
        PanelAnimator.SetTrigger("Close");


    }
    void DeactiveAll()
    {
        for (int i = 0; i < PanelAnimator.transform.childCount; i++)
        {
            PanelAnimator.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    [System.Serializable]
    public class InformationByType
    {
        public InformationType Type;
        public GameObject Panel;
        public LocalizedDynamicText text;
        public Button[] buttons;
    }
    
}
public enum InformationType
{
    Reward,Info,AdReward,FinishPanel
}
public enum PanelColor
{
    Normal,Alert,Succuss
}

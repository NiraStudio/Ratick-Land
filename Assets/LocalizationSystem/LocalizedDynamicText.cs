using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;
using UPersian.Components;

public class LocalizedDynamicText : MonoBehaviour {


    [SerializeField]
    public bool Checkable;
    public RtlText Text;
    string PersianText, EnglishText;

    LocalizationManager LM;
    bool Allow;
    // Use this for initialization
    void Start()
    {
        //yield return new WaitUntil(() => LocalizationManager.Instance.GetIsReady);
        LM = LocalizationManager.Instance;
        Text = GetComponent<RtlText>();
        Allow = true;

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!Allow)
            return;

        switch (LM.LanguageCode)
        {
            case Language.EN:
                Text.text = EnglishText;
                break;
            case Language.FA:
                Text.text = PersianText;
                break;
            
        }

        Text.font = LM.Font;
    }
    public void ChangeText(string persian,string english,bool checkable,bool ChangeNumbers)
    {
        if (ChangeNumbers)
            PersianText = LocalizationManager.LastChanger(persian);
        else
            PersianText = change(persian);
        
        EnglishText = english;
        Checkable = checkable;
        print("text changed "+english);
    }
    public string Number
    {
        set
        {
            PersianText = EnglishText = LocalizationManager.LastChanger(value);
        }
    }
    void Reset()
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            DestroyImmediate(gameObject.GetComponent<Text>());
        }
        if (gameObject.GetComponent<Text>() == null)
        {
            Text = gameObject.AddComponent<RtlText>();
        }
        else
        {
            Text = gameObject.GetComponent<RtlText>();
        }
        Text.resizeTextForBestFit = true;
        Text.alignment = TextAnchor.MiddleCenter;

    }
    string change(string text)
    {
        string a = "";
        char[] aa = text.ToCharArray();

        for (int i = 0; i < aa.Length; i++)
        {
            switch (aa[i])
            {
                case '۱':
                    a += "1";
                    break;
                case '۲':
                    a += "2";
                    break;
                case '۳':
                    a += "3";
                    break;
                case '۴':
                    a += "4";
                    break;
                case '۵':
                    a += "5";
                    break;
                case '۶':
                    a += "6";
                    break;
                case '۷':
                    a += "7";
                    break;
                case '۸':
                    a += "8";
                    break;
                case '۹':
                    a += "9";
                    break;
                case '۰':
                    a += "0";
                    break;

                default:
                    a += aa[i];
                    break;


            }
        }

        return a;
    }
}

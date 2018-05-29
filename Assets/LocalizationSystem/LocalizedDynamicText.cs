using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;
using TMPro;
public class LocalizedDynamicText : MonoBehaviour {

    public TextMeshProUGUI textMesh;
    [SerializeField]
    string PersianText, EnglishText;

    public string text {
        set
        {
            _text = value;
            oneTexted = true;
            textMesh.text = _text;
        }
    }

    string _text;
    bool oneTexted;
    LocalizationManager LM;
    bool Allow;
    Language lang;
    // Use this for initialization
    void Start()
    {
        LM = LocalizationManager.Instance;
        lang = LM.LanguageCode;
        textMesh.font = LM.Font;
        textMesh = GetComponent<TextMeshProUGUI>();
        Allow = true;

    }

    // Update is called once per frame
    void Update() {
        if (!Allow)
            return;
        if (lang != LM.LanguageCode)
        {
            if(oneTexted==false)
            check();
            else
            {
                textMesh.font = LM.Font;
                lang = LM.LanguageCode;
            }


        }

    }

    void check()
    {
        switch (LM.LanguageCode)
        {
            case Language.EN:
                textMesh.text = EnglishText;
                break;
            case Language.FA:
                textMesh.text = PersianText.faConvert();
                break;

        }
        textMesh.font = LM.Font;
        lang = LM.LanguageCode;
    }
    public void Text(string persian,string english)
    {
        PersianText = persian;
        EnglishText = english;
        oneTexted = false;
    }

    void Reset()
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            DestroyImmediate(gameObject.GetComponent<Text>());
        }
        if (gameObject.GetComponent<Text>() == null)
        {
            textMesh = gameObject.AddComponent<TextMeshProUGUI>();
        }
        else
        {
            textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        }
        textMesh.enableAutoSizing = true;
        textMesh.alignment = TextAlignmentOptions.Center;

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

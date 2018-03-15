using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
 
namespace Alpha.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        public string key;
        public string BeforeExtra, AfterExtra;
        public RtlText t;
        public bool Checkable=true;
        bool Allow;
        public string After
        {
            set
            {
                AfterExtra = LocalizationManager.LastChanger(value);
            }
        }
        public string Key
        {
            set
            {
                key = value;
            }
        }
        public string Before
        {
            set
            {
                BeforeExtra = LocalizationManager.LastChanger( value);
            }
        }


        LocalizationManager LM;
        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitUntil(() => LocalizationManager.Instance.GetIsReady);
            LM = LocalizationManager.Instance;
            if(key==null)
            t.text =BeforeExtra+ LM.GetLocalizationValue(key)  +AfterExtra;
            Allow = true;
           

        }

       void LateUpdate()
        {
            if (!Allow)
                return;
            t.text = BeforeExtra + LM.GetLocalizationValue(key) +  AfterExtra;
            switch (LM.LanguageCode)
            {
                case Language.EN:
                    t.font = LM.ENFont;
                    break;
                case Language.FA:
                    t.font = LM.FAFont;

                    break;
                
            }

        }

        void Reset()
        {
            if (gameObject.GetComponent<Text>() != null)
            {
                DestroyImmediate(gameObject.GetComponent<Text>());
                print("dasdas");
            }
            if (gameObject.GetComponent<Text>() == null)
            {
                t = gameObject.AddComponent<RtlText>();
            }
            else
            {
                t = gameObject.GetComponent<RtlText>();
            }
            t.resizeTextForBestFit = true;
            t.alignment = TextAnchor.MiddleCenter;
            
            Checkable = true;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
 
namespace Alpha.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        public string Key;
        public string BeforeExtra, AfterExtra;
        public RtlText t;
        public bool Checkable;
        bool Allow;
        public string ChangeAfter
        {
            set
            {
                AfterExtra = value;
            }
        }
        public string ChangeKey
        {
            set
            {
                Key = value;
            }
        }
        public string ChangeBefore
        {
            set
            {
                BeforeExtra = value;
            }
        }


        LocalizationManager LM;
        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitUntil(() => LocalizationManager.Instance.GetIsReady);
            LM = LocalizationManager.Instance;
            if(Key==null)
            t.text =BeforeExtra+ LM.GetLocalizationValue(Key)  +AfterExtra;
            Allow = true;
           

        }

       void LateUpdate()
        {
            if (!Allow)
                return;
            t.text = BeforeExtra + LM.GetLocalizationValue(Key) +  AfterExtra;
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
            switch (LM.LanguageCode)
            {
                case Language.EN:
                    t.font = LM.ENFont;
                    break;
                case Language.FA:
                    t.font = LM.FAFont;

                    break;

            }
            Checkable = true;
        }

    }
}
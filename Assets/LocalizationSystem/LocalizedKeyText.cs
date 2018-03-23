using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
 
namespace Alpha.Localization
{
    public class LocalizedKeyText : MonoBehaviour
    {
        [SerializeField]
        string key;
        public RtlText Text;
        public bool DefultSize;
        public bool UseCurrentParameters;
        public int PersianSize, EnglishSize;
        public string Key
        {
            set
            {
                key = value;
            }
            get
            {
                return key;
            }
        }

        LocalizationManager LM;
        bool Allow;
        int a;
        // Use this for initialization
        void Start()
        {
            LM = LocalizationManager.Instance;
            Text = GetComponent<RtlText>();
            if (!string.IsNullOrEmpty(key))
                Text.text = LM.GetLocalizationValue(key);

            Allow = true;
            Text.font = LM.Font;
        }

        void FixedUpdate()
        {
            if (!Allow)
                return;


            Text.text = string.Concat(LM.GetLocalizationValue(key));
            if (UseCurrentParameters)
            {
                switch (LM.LanguageCode)
                {
                    case Language.EN:
                        Text.resizeTextMaxSize = EnglishSize;
                        break;
                    case Language.FA:
                        Text.resizeTextMaxSize = PersianSize;

                        break;
                }
            }
            else if (DefultSize)
            {
                switch (LM.LanguageCode)
                {
                    case Language.EN:
                        Text.resizeTextMaxSize = 20;
                        break;
                    case Language.FA:
                        Text.resizeTextMaxSize = 25;

                        break;
                }
            }
        
            Text.font = LM.Font;

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
       

    }
}
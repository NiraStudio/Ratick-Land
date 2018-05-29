using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Alpha.Localization
{
    public class LocalizedKeyText : MonoBehaviour
    {
        [SerializeField]
        string key;
        public TextMeshProUGUI textMesh;
        void Awake()
        {
            LM = LocalizationManager.Instance;
        }
        public string Key
        {
            set
            {
                key = value;
                
                textMesh.text = LM.GetLocalizationValue(key).faConvert();
            }
            get
            {
                return key;
            }
        }
        Language lang;
        LocalizationManager LM;
        bool Allow;
        int a;
        // Use this for initialization
        void Start()
        {
            LM = LocalizationManager.Instance;
            lang = LM.LanguageCode;
            textMesh = GetComponent<TextMeshProUGUI>();
            if (!string.IsNullOrEmpty(key))
                textMesh.text = LM.GetLocalizationValue(key).faConvert();
            Allow = true;
            textMesh.font = LM.Font;
        }

        void Update()
        {
            if (!Allow)
                return;


            if (lang != LM.LanguageCode)
            {

                if (LM.LanguageCode == Language.FA)
                    textMesh.text = LM.GetLocalizationValue(key).faConvert();
                else
                    textMesh.text = LM.GetLocalizationValue(key);




                textMesh.font = LM.Font;
                lang = LM.LanguageCode;

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
                textMesh = gameObject.AddComponent<TextMeshProUGUI>();
            }
            else
            {
                textMesh = gameObject.GetComponent<TextMeshProUGUI>();
            }
            textMesh.enableAutoSizing = true;
            textMesh.alignment = TextAlignmentOptions.Center;
            
        }
       

    }
}
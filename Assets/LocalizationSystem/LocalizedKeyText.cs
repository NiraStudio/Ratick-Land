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

        public string Key
        {
            set
            {
                key = value;
            }
        }

        LocalizationManager LM;
        bool Allow;
        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitUntil(() => LocalizationManager.Instance.GetIsReady);
            LM = LocalizationManager.Instance;
            Text = GetComponent<RtlText>();
            Text.text = LM.GetLocalizationValue(key);

            Allow = true;
            Text.font = LM.Font;
        }

       void FixedUpdate()
        {
            if (!Allow)
                return;
            Text.text = LM.GetLocalizationValue(key);
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
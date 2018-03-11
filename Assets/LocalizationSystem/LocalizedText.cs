using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
 
namespace Nira.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        public string Key;
        public string BeforeExtra, AfterExtra;
        public RtlText t;
        public bool Checkable;
        bool Allow;
        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitUntil(() => LocalizationManager.Instance.GetIsReady);
            t.text =BeforeExtra+ LocalizationManager.Instance.GetLocalizationValue(Key)  +AfterExtra;

            Allow = true;
           

        }

       void LateUpdate()
        {
            if (!Allow)
                return;
            t.text = BeforeExtra +  LocalizationManager.Instance.GetLocalizationValue(Key) +  AfterExtra;

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
        }

    }
}
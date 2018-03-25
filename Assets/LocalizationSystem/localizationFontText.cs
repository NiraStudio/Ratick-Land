using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

namespace Alpha.Localization
{
    public class localizationFontText : MonoBehaviour
    {
        public string text;

        RtlText Text;
        LocalizationManager LM;
        bool Allow;
        // Use this for initialization
        void Start()
        {
            LM = LocalizationManager.Instance;
            Text = GetComponent<RtlText>();
            Allow = true;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Text.font = LM.Font;

            Text.text = text;
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
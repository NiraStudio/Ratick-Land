using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nira.Localization
{
    [System.Serializable]
    public class LocalizationData
    {

        public LocalizationItem[] Data=new LocalizationItem[0];
    }
    [System.Serializable]
    public class LocalizationItem
    {
        public string Key;
        public string Value;
    }
}
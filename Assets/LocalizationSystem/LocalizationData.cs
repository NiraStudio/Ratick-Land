using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Alpha.Localization
{
    [System.Serializable]
    public class LocalizationData
    {

        public List<LocalizationItem> Data = new List<LocalizationItem>();

        public void Remove(string Key)
        {
            foreach (var item in Data)
            {
                if(item.Key==Key)
                {
                    Data.Remove(item);
                    break;
                }
            }
        }
    }
    [System.Serializable]
    public class LocalizationItem
    {
        public string Key;
        public string Value;
    }
}
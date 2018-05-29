using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Alpha.Localization {
    public class LocalizationDatabase : ScriptableObject {
        public List<LocalizationDataByLanguage> Languages = new List<LocalizationDataByLanguage>();

        public LocalizationData Data(Language language)
        {
            LocalizationData a = new LocalizationData();
            foreach (var item in Languages.ToArray())
            {
                if (item.language == language)
                    return item.data;
            }
            return a;
        }
        public void RemoveItem(string Key)
        {
            for (int i = 0; i < Languages.Count; i++)
            {
                Languages[i].data.Remove(Key);
            }
        }
        public void Add(string key,string value,Language language)
        {
            LocalizationItem a = new LocalizationItem();
            a.Key = key;
            a.Value = value;
            
            foreach (var item in Languages.ToArray())
            {
                if (item.language == language)
                {
                    item.data.Data.Add(a);
                    return;
                }
            }
            LocalizationDataByLanguage tt = new LocalizationDataByLanguage();
            tt.language = language;
            tt.data = new LocalizationData();
            Languages.Add(tt);
            foreach (var item in Languages.ToArray())
            {
                if (item.language == language)
                {
                    item.data.Data.Add(a);
                    return;
                }
            }
        }
        public string GiveValue(string key, Language language)
        {
            LocalizationData a = Data(language);
            foreach (var item in a.Data)
            {
                if(item.Key==key)
                {
                    return item.Value;
                }
            }
            return null;
        }
        public bool ContainKey(string key)
        {
            bool a=false;
            foreach (var item in Languages[0].data.Data)
            {
                if (item.Key == key)
                {
                    a = true;
                    break;
                }
            }

            return a;
        }
        public void setDirty()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    [System.Serializable]
    public class LocalizationDataByLanguage
    {
        public Language language;
        public LocalizationData data;
    }

}
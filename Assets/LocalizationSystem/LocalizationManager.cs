using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace Alpha.Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        #region Singleton
        public static LocalizationManager Instance;
        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
        public Dictionary<string, string> LocalizationText = new Dictionary<string, string>();
        public Language LanguageCode;
        bool IsReady = false;

        public Font ENFont, FAFont;

        void Start()
        {
            LoadData(LanguageCode);
        }
        public void LoadData(Language language)
        {

            string FileName = language.ToString() + ".json";
            string filePath;

            #region Path

#if UNITY_EDITOR
            filePath = Path.Combine(Application.streamingAssetsPath, FileName);

#elif UNITY_IOS
         filePath = Path.Combine (Application.dataPath + "/Raw", FileName);
 
#elif UNITY_ANDROID
         filePath = Path.Combine ("jar:file://" + Application.dataPath + "!assets/", FileName);
 
#endif

            #endregion


            string dataAsJSON = null;

            #region GetData
#if UNITY_EDITOR || UNITY_IOS
            if (File.Exists(filePath))
            {
                dataAsJSON = File.ReadAllText(filePath);
            }
#elif UNITY_ANDROID
            WWW reader = new WWW (filePath);
            while (!reader.isDone) {
            }
            dataAsJSON = reader.text;
#endif
            #endregion
            if (!string.IsNullOrEmpty(dataAsJSON))
            {
                LocalizationText = new Dictionary<string, string>();


                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJSON);
                foreach (var item in loadedData.Data)
                {
                    LocalizationText.Add(item.Key, item.Value);
                }
                Debug.Log("Data Loaded , Dictionary Contains " + LocalizationText.Count + " Enteries");
                IsReady = true;
            }
            else
                Debug.LogError("Cannot Find Data");
        }
            











    public bool GetIsReady
    {
        get { return IsReady; }
    }
    public string GetLocalizationValue(string Key)
    {
            if (string.IsNullOrEmpty(Key))
                return null;

        if (LocalizationText.ContainsKey(Key))
            return LocalizationText[Key];
        else
            return "Localization Text Not Find";
    }


}
    public enum Language
    {
        EN, FA
    }
}
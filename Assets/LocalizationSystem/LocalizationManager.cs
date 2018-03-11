using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace Nira.Localization
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



        void Start()
        {
            LoadData(LanguageCode);
        }


        public void LoadData(Language language)
        {
            string FileName = language.ToString()+ ".json";
            LocalizationText = new Dictionary<string, string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, FileName);

            if (File.Exists(filePath))
            {
                string dataAsJSON = File.ReadAllText(filePath);

                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJSON);
                foreach (var item in loadedData.Data)
                {
                    LocalizationText.Add(item.Key, item.Value);
                }
                Debug.Log("Data Loaded , Dictionary Contains " + LocalizationText.Count + "Enteries");
            }
            else
            {
                Debug.LogError("Cannot Find Data");
            }
            IsReady = true;
        }
        public bool GetIsReady
        {
            get { return IsReady; }
        }
        public string GetLocalizationValue(string Key)
        {
            if (LocalizationText.ContainsKey(Key))
                return LocalizationText[Key];
            else
                return "Localization Text Not Find";
        }

        
    }
    public enum Language
    {
        EN,FA
    }
}
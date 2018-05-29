using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alpha.Localization;

public class SettingManager : MonoBehaviour {

    #region Singleton
    public static SettingManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public bool SFXMute, BGMMute;
    public Language l;
    void Start()
    {
        bool.TryParse( PlayerPrefs.GetString("SFXMute"), out SFXMute);

        bool.TryParse( PlayerPrefs.GetString("BGMMute"), out BGMMute);
    }
    // Use this for initialization
    void Update()
    {
        l = LocalizationManager.Instance.LanguageCode;
    }

    public void ChangeSFX(bool state)
    {
        SFXMute = state;
        PlayerPrefs.SetString("SFXMute", SFXMute.ToString());
    }
    public void ChangeBGM(bool state)
    {
        BGMMute = state;
        PlayerPrefs.SetString("BGMMute", BGMMute.ToString());

    }
    public void ChangeLanguage(bool a)
    {
        if (a==false)
            LocalizationManager.Instance.LanguageCode = Language.FA;
        else 
            LocalizationManager.Instance.LanguageCode = Language.EN;

    }
}

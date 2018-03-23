using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {
    public Toggle LanguageToggle, BGMToggle,SFXToggle;

    Animator anim;
    SettingManager Sm;
    bool open;
	// Use this for initialization
	void Start () {
        Sm = SettingManager.Instance;
        anim = GetComponent<Animator>();


        LanguageToggle.isOn = Alpha.Localization.LocalizationManager.Instance.LanguageCode == Alpha.Localization.Language.EN ? true : false;
        BGMToggle.isOn = Sm.BGMMute;
        SFXToggle.isOn = Sm.SFXMute;
        LanguageToggle.onValueChanged.AddListener(ChangeLanguage);
        BGMToggle.onValueChanged.AddListener(ChangeBGM);
        SFXToggle.onValueChanged.AddListener(ChangeSFX);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeLanguage(bool state)
    {
        Sm.ChangeLanguage(state);
    }
    public void ChangeSFX(bool mute)
    {
        Sm.ChangeSFX(mute);
    }
    public void ChangeBGM(bool mute)
    {
        Sm.ChangeBGM(mute);
    }
    public void OpenAndClose()
    {
        anim.SetTrigger(open ? "Close" : "Open");
        open = !open;
    }
}

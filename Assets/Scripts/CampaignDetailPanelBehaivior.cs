using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class CampaignDetailPanelBehaivior : MonoBehaviour {
    public LocalizedDynamicText AttackDamage, AttackSpeed, HitPoint;public LocalizedKeyText CharacterName;
    public Slider SpeedBar;
    DetailState state,skinState;
    GameManager GM;
    void Start()
    {
        SpeedBar.maxValue = 3;
        GM = GameManager.instance;
    }
	// Use this for initialization
	public void RePaint(CharacterData data,Skin skin)
    {
        CharacterName.Key = data.characterName;
        state = GM.CharacterState(data);
        skinState = skin.State();
        AttackDamage.Number = state.AttackDamage.ToString();
        float a = 3 - state.AttackSpeed;
        SpeedBar.value = a;
        HitPoint.Number = state.HitPint.ToString();
    }
}
[System.Serializable]
public class DetailState
{
    public int AttackDamage, HitPint;
    public float AttackSpeed;
}

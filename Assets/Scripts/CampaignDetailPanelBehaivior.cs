using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alpha.Localization;

public class CampaignDetailPanelBehaivior : MonoBehaviour {
    public LocalizedText AttackDamage, AttackSpeed, HitPoint,CharacterName;

    DetailState state,skinState;
    GameManager GM;
    void Start()
    {
        GM = GameManager.instance;
    }
	// Use this for initialization
	public void RePaint(CharacterData data,Skin skin)
    {
        CharacterName.Key = data.characterName;
        state = GM.CharacterState(data);
        skinState = skin.State();
        AttackDamage.Before = state.AttackDamage.ToString()+"+("+skinState.AttackDamage;
        AttackSpeed.Before = state.AttackSpeed.ToString() + "+(" + skinState.AttackSpeed;
        HitPoint.Before = state.HitPint.ToString() + "+(" + skinState.HitPint;
        AttackDamage.key = AttackSpeed.key = HitPoint.key = "SkinPower";
        AttackDamage.After = AttackSpeed.After = HitPoint.After = ")";
    }
}
[System.Serializable]
public class DetailState
{
    public int AttackDamage, HitPint;
    public float AttackSpeed;
}

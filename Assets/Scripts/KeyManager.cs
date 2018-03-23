using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {


    #region Singleton
    public static KeyManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public GameObject PSObject;
    public int keyCount = 3, GotedKey;
    public int KeyPartGeted, KeyPartNeeded;
    // Use this for initialization
    void Start () {
        RenewKeyPartAmount();
	}
	
	// Update is called once per frame
	void Update () {
        if (KeyPartGeted >= KeyPartNeeded)
        {
            ChangeKeyCount(1);
            RenewKeyPartAmount();
            PSObject.gameObject.SetActive(true);
        }
    }
    public void RenewKeyPartAmount()
    {
        KeyPartGeted = KeyPartGeted - KeyPartNeeded;
        ChangeKeyPartNeeded();
    }
    public void ChangeKeyPartNeeded()
    {
        KeyPartNeeded = 0;
        KeyPartNeeded = 5 + (GotedKey * 10);
    }
    public void ChangeKeyCount(int amount)
    {
        keyCount += amount;
        if (amount > 0)
            GotedKey += amount;
    }

    public void ChangeKeyPart(int amount)
    {
        KeyPartGeted += amount;
    }
}

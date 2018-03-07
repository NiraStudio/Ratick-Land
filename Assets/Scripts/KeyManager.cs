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

    public int keyCount = 3, GotedKey;
    public int KeyPartGeted, KeyPartNeeded;
    // Use this for initialization
    void Start () {
        RenewKeyPartNeeded();
	}
	
	// Update is called once per frame
	void Update () {
        if (KeyPartGeted >= KeyPartNeeded)
        {
            ChangeKeyCount(1);
            RenewKeyPartNeeded();
        }
    }
    public void RenewKeyPartNeeded()
    {
        KeyPartGeted = KeyPartGeted - KeyPartNeeded;
        ChangeKeyPartNeeded();
    }
    public void ChangeKeyPartNeeded()
    {
        KeyPartNeeded = 0;
        KeyPartNeeded = 15 + (GotedKey * 15);
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

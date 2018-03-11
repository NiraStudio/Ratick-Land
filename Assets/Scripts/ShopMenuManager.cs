using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour {

    #region Singleton
    public static ShopMenuManager Instance;

    void Awake()
    {
        Instance = this;
    }
    #endregion
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenChest(string ChestName)
    {
        ChestManager.Instance.OpenChest(ChestName);
    }
}

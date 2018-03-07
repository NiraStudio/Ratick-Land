using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDB :MonoBehaviour  {
    public List<Skin> skins = new List<Skin>();

    public List<Skin> GiveSkins
    {
        get { return skins; }
    }
	
}

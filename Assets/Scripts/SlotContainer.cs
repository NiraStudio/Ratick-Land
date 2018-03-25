using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotContainer  {
    public int minionId, minionCount,supportId,supoortLevel,mainId,mainLevel;
    public List<int> Heros = new List<int>();
    public Potion porion;

    public void ChangeHeros(List<int> heros)
    {
        Heros = heros;
    }
    public void ChangeMinion(int ID, int Level)
    {
        minionId = ID;
        minionCount = Level;
    }
    public void ChangeSupport(int ID, int Level)
    {
        supportId = ID;
        supoortLevel = Level;
    }
    public void ChangeMain(int ID, int Level)
    {
        mainId = ID;
        mainLevel = Level;
    }
    
    
}

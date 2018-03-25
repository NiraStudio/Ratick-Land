using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeSceneCardBtn : MonoBehaviour {
    public Potion.Type type;
    public ArrangeSceneManager manager;

    void Start()
    {
        manager = ArrangeSceneManager.Instance;
    }
    public void Onclick()
    {
        manager.CloseCardPanel(new Potion(type, 1));
    }
}

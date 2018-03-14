using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StringDataBase))]
public class StringDataBaseEditor : Editor
{
    int amount, length;
    StringDataBase d;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        d = (StringDataBase)target;

        GUILayout.Space(20);

        GUILayout.Label("Number of elements");
        amount = EditorGUILayout.IntField(amount);

        GUILayout.Label("length of elements");
        length = EditorGUILayout.IntField(length);

        if (GUILayout.Button("Create Elements"))
        {
            for (int i = 0; i < amount; i++)
            {
                d.DB.Add(GiveID());
            }
        }
    }
    public string GiveID()
    {
        string answer = "";
        bool find = true;

        do
        {
            find = true;
            answer = "";

            for (int i = 0; i < length; i++)
            {
                int a = Random.Range(1, 3);
                if (a == 1)
                {
                    answer += (char)Random.Range(48, 58);
                }
                else
                {
                    answer += (char)Random.Range(65, 90);
                }
            }

            foreach (var item in d.DB)
            {
                if (item == answer)
                {
                    find = false;
                    break;
                }
            }
        } while (find == false);

        return answer;
    }
}

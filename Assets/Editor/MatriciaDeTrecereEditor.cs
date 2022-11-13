using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

[CustomEditor(typeof(MatriceaDeTrecere))]
public class MatriciaDeTrecereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MatriceaDeTrecere sef = (MatriceaDeTrecere)target;

        if (GUILayout.Button("Baza Noua"))
        {
            sef.AddNewBase();
        }

        if (GUILayout.Button("Update"))
        {
            sef.Update();
        }

        for(int i = 0; i < sef.baze.Count; i++)
        {
            if (sef.baze[i].nume == "Baza noua")
            {
                if (GUILayout.Button("Sterge baza " + i))
                {
                    sef.DeleteBase(i);
                }
            }
            else
            {
                if (GUILayout.Button("Sterge baza " + sef.baze[i].nume))
                {
                    sef.DeleteBase(i);
                }
            }

            
        }
    }
}

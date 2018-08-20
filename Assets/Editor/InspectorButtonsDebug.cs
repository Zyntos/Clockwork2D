using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CharController))]
public class InspectorButtonsDebug : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharController myScript = (CharController)target;
        if (GUILayout.Button("AddSkill"))
        {
            myScript.AddSkill();
        }
    }
    
}
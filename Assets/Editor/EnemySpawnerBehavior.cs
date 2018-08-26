using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerBehavior : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemySpawner myScript = (EnemySpawner)target;
        if (GUILayout.Button("SpawnEnemy"))
        {
            myScript.Spawn();
        }
    }

}
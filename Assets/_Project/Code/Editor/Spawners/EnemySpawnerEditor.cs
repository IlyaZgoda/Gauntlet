using Code.Logic.Spawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Spawners
{

    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            EnemySpawner spawner = (EnemySpawner)target;
            Handles.color = Color.red;
            Handles.DrawSolidDisc(spawner.transform.position, Vector3.forward, 0.5f);
        }
    }
}

using Code.Logic.Spawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Spawners
{

    [CustomEditor(typeof(HeroSpawner))]
    public class HeroSpawnerEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            HeroSpawner spawner = (HeroSpawner)target;
            Handles.color = Color.green;
            Handles.DrawSolidDisc(spawner.transform.position, Vector3.forward, 0.5f);
        }
    }
}

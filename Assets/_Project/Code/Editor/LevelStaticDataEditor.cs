using Code.Logic.Spawners;
using Code.StaticData.SceneManagement.Spawners;
using Code.StaticData.SceneManagement;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelStaticData levelStaticData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelStaticData.EnemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None).
                    Select(x => new EnemySpawnerStaticData(x.transform.position)).
                    ToList();

                var heroSpawner = Object.FindAnyObjectByType<HeroSpawner>();
                levelStaticData.HeroSpawner = new HeroSpawnerStaticData(heroSpawner.transform.position);

            }

            EditorUtility.SetDirty(target);

        }
    }
}

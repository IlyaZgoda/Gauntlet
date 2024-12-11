using Code.StaticData.SceneManagement.Spawners;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.SceneManagement
{
    [CreateAssetMenu(fileName = "LevelData",menuName ="StaticData/Levels", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public HeroSpawnerStaticData HeroSpawner;
        public List<EnemySpawnerStaticData> EnemySpawners;
    }
}

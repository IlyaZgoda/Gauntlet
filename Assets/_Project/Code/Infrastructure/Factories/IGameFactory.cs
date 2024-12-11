using Code.Logic.Hero;
using Code.StaticData.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreateHero(string path, LevelStaticData levelStaticData);
        GameObject CreateEnemy(string path, Vector3 position);
        List<GameObject> CreateEnemyWave(int count, LevelStaticData levelStaticData);
    }
}
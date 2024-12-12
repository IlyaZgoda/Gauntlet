using Code.Logic;
using Code.Logic.Hero;
using Code.StaticData.SceneManagement;
using Code.UI.HUD;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreateHero(string path, LevelStaticData levelStaticData);
        GameObject CreateEnemy(string path, Vector3 position);
        EnemyWave CreateEnemyWave(int count, LevelStaticData levelStaticData);
        UniTask<ActorUI> CreateHUD();
        HealthPack CreateHealthPack(string path, Vector3 position);
    }
}
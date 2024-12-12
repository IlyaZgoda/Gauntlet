using Code.Logic;
using Code.Logic.Enemy;
using Code.Logic.Hero;
using Code.Services.StaticData;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Code.UI.HUD;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly DiContainer _container;
        private GameObject _hero;

        public GameFactory(DiContainer container, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _container = container;
        }

        public GameObject CreateHero(string path, LevelStaticData levelStaticData)
        {
            var prefab = Resources.Load<GameObject>(path);
            var instance = GameObject.Instantiate(prefab, levelStaticData.HeroSpawner.Position, prefab.transform.rotation);

            _container.InjectGameObject(instance);

            return _hero = instance;
        }

        public GameObject CreateEnemy(string path, Vector3 position)
        {
            var heroDeath = _hero.GetComponent<HeroDeath>();

            var prefab = Resources.Load<GameObject>(path);
            var instance = GameObject.Instantiate(prefab, position, prefab.transform.rotation);

            instance.GetComponent<AgentMoveToPlayer>().Construct(_hero.transform);
            instance.GetComponent<StopPursuit>().Construct(heroDeath);

            _container.InjectGameObject(instance);
            
            return instance;
        }

        public EnemyWave CreateEnemyWave(int totalEnemies, LevelStaticData levelStaticData)
        {
            List<GameObject> enemies = new();

            var random = new System.Random();
            var spawners = levelStaticData.EnemySpawners;
            var randomSpawners = spawners.OrderBy(x => random.Next()).Take(5).ToList();

            int enemiesPerSpawner = totalEnemies / randomSpawners.Count;
            int remainingEnemies = totalEnemies % randomSpawners.Count;

            foreach (var spawner in randomSpawners)
            {
                int enemiesToSpawn = enemiesPerSpawner;

                if (remainingEnemies > 0)
                {
                    enemiesToSpawn++;
                    remainingEnemies--;
                }

                for (int j = 0; j < enemiesToSpawn; j++)
                {
                    var enemy = CreateEnemy(ResourcesAssetPath.Enemy, spawner.Position);
                    enemies.Add(enemy);
                }
            }

            return new EnemyWave(enemies);
        }

        public async UniTask<ActorUI> CreateHUD()
        {
            var hud = _instantiator.InstantiatePrefabResourceForComponent<ActorUI>("HUD/hud");
            await UniTask.Yield();

            return hud;
        }

        public HealthPack CreateHealthPack(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            var instance = GameObject.Instantiate(prefab, position, prefab.transform.rotation);
            var healthPack = instance.GetComponent<HealthPack>();

            return healthPack;
        }

    }
}

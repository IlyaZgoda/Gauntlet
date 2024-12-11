using Code.Logic.Enemy;
using Code.Logic.Hero;
using Code.Services.StaticData;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
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

        public List<GameObject> CreateEnemyWave(int count, LevelStaticData levelStaticData)
        {
            List<GameObject> enemies = new();

            for (int i = 0; i < levelStaticData.EnemySpawners.Count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    var enemy = CreateEnemy(ResourcesAssetPath.Enemy, levelStaticData.EnemySpawners[j].Position);

                    enemies.Add(enemy);
                }  
            }

            return enemies;
        }

    }
}

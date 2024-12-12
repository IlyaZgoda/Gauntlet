using Code.Infrastructure.Factories;
using Code.Services.Observable;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    public class EnemyWaveHandler 
    {
        private IGameFactory _gameFactory;
        private LevelStaticData _levelStaticData;
        private CoreEventBus _eventBus;
        private EnemyWave _current;
        private HealthPack _healthPack;
        private int _increment;
        private int _currentCount;
        private int _waveCount = 1;

        public EnemyWaveHandler(IGameFactory gameFactory, LevelStaticData levelStaticData, CoreEventBus eventBus)
        {
            _gameFactory = gameFactory;
            _levelStaticData = levelStaticData;
            _eventBus = eventBus;
        } 

        private void SpawnWave(int count)
        {
            _current = _gameFactory.CreateEnemyWave(count, _levelStaticData);
            _currentCount = count;
            _eventBus.WaveSpawned?.Invoke(_waveCount);
            _waveCount++;

            SpawnersHealthPack();
        }

        private void SpawnersHealthPack()
        {
            System.Random rnd = new();
            var spawnerNumber = rnd.Next(_levelStaticData.EnemySpawners.Count);
            _healthPack = _gameFactory.CreateHealthPack(ResourcesAssetPath.HealthPack, _levelStaticData.EnemySpawners[spawnerNumber].Position);
        }

        private bool CanSpawn() =>
            _current?.WaveCount == 0;

        public async UniTask HandleAsync(int initCount, int increment)
        {
            SpawnWave(initCount);
            _increment = increment;

            while (true)
            {
                await UniTask.Delay(TimeSpan.FromMinutes(0.1));
                if (CanSpawn())
                {
                    _current.Dispose();
                    UnityEngine.Object.Destroy(_healthPack.gameObject);
                    SpawnWave(_currentCount + _increment);
                }
            }
        }
    }
}

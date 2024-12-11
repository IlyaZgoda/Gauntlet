using Code.Infrastructure.Factories;
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
        private EnemyWave _current;
        private int _increment;
        private int _currentCount;

        public EnemyWaveHandler(IGameFactory gameFactory, LevelStaticData levelStaticData)
        {
            _gameFactory = gameFactory;
            _levelStaticData = levelStaticData;
        } 

        private void SpawnWave(int count)
        {
            _current = _gameFactory.CreateEnemyWave(count, _levelStaticData);
            _currentCount = count;
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
                    SpawnWave(_currentCount + _increment);
                }
            }
        }
    }
}

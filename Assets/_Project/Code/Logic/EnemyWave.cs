using Code.Logic.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Logic
{
    public class EnemyWave : IDisposable
    {
        private List<GameObject> _enemies;
        private int _waveCount;
        public int InitWaveCount { get; private set; }
        public int WaveCount {  get => _waveCount; private set => _waveCount = value; }

        public EnemyWave(List<GameObject> enemies)
        {
            _enemies = enemies;
            InitWaveCount = enemies.Count;
            _waveCount = InitWaveCount;

            SubscribeEnemies();
        }

        private void SubscribeEnemies()
        {
            foreach (var enemy in _enemies)
                enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDeath; 
        }

        private void OnEnemyDeath()
        {
            WaveCount--;
        }

        public void Dispose()
        {
            
        }
    }
}

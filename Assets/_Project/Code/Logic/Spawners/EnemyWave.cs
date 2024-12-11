using Code.Infrastructure.Factories;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Logic.Spawners
{
    public class EnemyWave
    {
        private IGameFactory _gameFactory;
        private LevelStaticData _levelStaticData;

        public EnemyWave(IGameFactory gameFactory, LevelStaticData levelStaticData)
        {
            _gameFactory = gameFactory;
            _levelStaticData = levelStaticData;
        }

        
    }
}

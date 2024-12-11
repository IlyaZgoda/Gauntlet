using Code.Infrastructure.Factories;
using Code.Logic;
using Code.Services.Observable;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IPayloadedState<LevelStaticData>
    {
        private IGameFactory _gameFactory;
        private CoreEventBus _eventBus;
        private LevelStaticData _levelStaticData;

        public GameLoopState(IGameFactory gameFactory, CoreEventBus eventBus)
        {
            _gameFactory = gameFactory;
            _eventBus = eventBus;
        }

        public async UniTask Enter(LevelStaticData levelStaticData)
        {
            _levelStaticData = levelStaticData;

            EnemyWaveHandler handler = new(_gameFactory, _levelStaticData);
            await handler.HandleAsync(5, 3);

            await UniTask.Yield();
        }
        
        
        public UniTask Exit() =>
            default;          
    }
}

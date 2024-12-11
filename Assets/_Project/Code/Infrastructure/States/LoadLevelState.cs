using Code.Infrastructure.Factories;
using Code.Infrastructure.SceneManagement;
using Code.Logic;
using Code.Logic.Hero;
using Code.Services.Observable;
using Code.Services.StaticData;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingProgressPresenter _loadingProgress;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly CoreEventBus _coreEventBus;

        public LoadLevelState(IGameStateMachine gameStateMachine, 
            ISceneLoader sceneLoader, 
            LoadingProgressPresenter 
            loadingProgress, 
            IStaticDataService staticDataService, 
            IGameFactory gameFactory,
            CoreEventBus coreEventBus)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingProgress = loadingProgress;
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _coreEventBus = coreEventBus;
        }
        public async UniTask Enter(string payload)
        {
            await _sceneLoader.Load(Scenes.Loading);
            await _sceneLoader.Load(payload, _loadingProgress, EnterLoadLevel);
        }

        private async UniTask EnterLoadLevel()
        {
            LevelStaticData levelStaticData = Resources.Load<LevelStaticData>("StaticData/Level/LevelData");
            var hero = _gameFactory.CreateHero(ResourcesAssetPath.Hero, levelStaticData);

            Camera.main.GetComponent<CameraFollow>().Follow(hero);
            _gameFactory.CreateEnemyWave(2, levelStaticData);

            await _gameStateMachine.Enter<GameLoopState>();
        }       
            
        public UniTask Exit() =>
            default;
    }
}

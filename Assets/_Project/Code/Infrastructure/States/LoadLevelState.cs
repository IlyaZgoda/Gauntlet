using Code.Infrastructure.Factories;
using Code.Infrastructure.SceneManagement;
using Code.Services.Observable;
using Code.Services.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using Infrastructure.States;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IState
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
        public async UniTask Enter()
        {
            await _sceneLoader.Load(Scenes.Loading);
            await _sceneLoader.Load(Scenes.Core, _loadingProgress, EnterLoadLevel);
        }

        private async UniTask EnterLoadLevel()
        {
            await _gameStateMachine.Enter<GameLoopState>();
        }       
            

        public UniTask Exit() =>
            default;    
    }
}

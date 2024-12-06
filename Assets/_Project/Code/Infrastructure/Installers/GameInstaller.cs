using Code.Infrastructure.Factories;
using Code.Infrastructure.SceneManagement;
using Code.Services.StaticData;
using Code.StaticData;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapperFactory();
            BindCoroutineRunner();
            BindSceneLoader();
            BindLoadingProgress();
            BindGameStateMachine();
            BindStaticDataService();
            BindGameFactory();
            BindCore();
        }

        private void BindBootstrapperFactory()
        {
            Container.
                Bind<BootstrapperFactory>().
                AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container.
                Bind<ICoroutineRunner>().
                To<CoroutineRunner>().
                FromComponentInNewPrefabResource(ResourcesAssetPath.CoroutineRunner).
                AsSingle();
        }

        private void BindGameStateMachine()
        {
            GameStateMachineInstaller.Install(Container);
        }

        private void BindCore()
        {
            CoreInstaller.Install(Container);
        }

        private void BindSceneLoader()
        {
            Container.
                BindInterfacesAndSelfTo<SceneLoader>().
                AsSingle();
        }

        private void BindLoadingProgress()
        {
            Container.
                BindInterfacesAndSelfTo<LoadingProgressPresenter>().
                AsSingle();
        }

        private void BindStaticDataService()
        {
            Container.
                BindInterfacesTo<StaticDataService>().
                AsSingle();
        }

        private void BindGameFactory()
        {
            Container.
                BindInterfacesAndSelfTo<GameFactory>().
                AsSingle();
        }
    }
}


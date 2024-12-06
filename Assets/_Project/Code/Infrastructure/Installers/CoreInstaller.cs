using Code.Services.Observable;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class CoreInstaller : Installer<CoreInstaller>
    {
        public override void InstallBindings()
        {
            BindCoreEventBus();
        }
        private void BindCoreEventBus()
        {
            Container.
                BindInterfacesAndSelfTo<CoreEventBus>().
                AsSingle();
        }
    }
}

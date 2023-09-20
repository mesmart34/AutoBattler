using DG.Tweening;
using Factories;
using Installers;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<UnitFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle().NonLazy();
        }

        public void Initialize()
        {
            Container.Resolve<UnitFactory>().Load();
            DOTween.Init();
        }
    }
}

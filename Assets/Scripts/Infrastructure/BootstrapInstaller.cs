using DG.Tweening;
using Factories;
using Scripts.Signals;
using Signals;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<StartBattleSignal>();
            Container.DeclareSignal<UnitDieSignal>();
            Container.DeclareSignal<StopBattleSignal>();
            Container.DeclareSignal<UnitPositionChangeSignal>();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle().NonLazy();
        }

        public void Initialize()
        {
            DOTween.Init();
        }
    }
}

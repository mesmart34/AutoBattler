using Models;

namespace Installers
{
    public class EnemyInstaller : UnitInstaller
    {
        protected override void BindUnit()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyModel>()
                .AsSingle()
                .WithArguments(unitSettings)
                .NonLazy();
            Container
                .BindInterfacesTo<EnemyInstaller>()
                .FromInstance(this)
                .AsSingle();
        }
    }
}
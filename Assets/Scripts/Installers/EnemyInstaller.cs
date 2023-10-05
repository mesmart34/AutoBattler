using Common.Unit;
using Models;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : UnitInstaller
    {
        protected override void BindUnit()
        {
            Container
                .Bind(typeof(UnitModel), typeof(IInitializable)).To<EnemyModel>()
                .AsSingle()
                .WithArguments(unitSettings)
                .NonLazy();
            Container
                .BindInterfacesTo<EnemyInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        public EnemyInstaller(UnitData unitData) : base(unitData)
        {
        }
    }
}
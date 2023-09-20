using Common.Unit;
using Contracts;
using Models;
using Zenject;

namespace Installers
{
    public class HeroInstaller : UnitInstaller
    {
        protected override void BindUnit()
        {
            Container
                .Bind(typeof(UnitModel), typeof(IInitializable)).To<HeroModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesTo<HeroInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        public HeroInstaller(UnitData unitData) : base(unitData)
        {
            
        }
    }
}
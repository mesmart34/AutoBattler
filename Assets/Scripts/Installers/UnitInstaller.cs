using Common.Unit;
using Factories;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public abstract class UnitInstaller : MonoInstaller, IInitializable
    {
        [InjectOptional]
        private UnitData _unitData;
        
        [SerializeField]
        public UnitSettings unitSettings;
        

        protected UnitInstaller(UnitData unitData)
        {
            _unitData = unitData;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(unitSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitHighlighter>().AsSingle().NonLazy();
            BindUnit();
            BindUnitServices();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<DamagePopupFactory>().AsSingle().NonLazy();
            Container.Bind<EffectFactory>().AsSingle().NonLazy();
        }

        private void BindUnitServices()
        {
            Container
                .BindInterfacesAndSelfTo<HealthService>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<ManaService>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<AttackService>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<AnimationService>()
                .AsSingle()
                .NonLazy();
        }

        protected abstract void BindUnit();

        public void Initialize()
        {
            var factory = Container.Resolve<DamagePopupFactory>();
            factory.Load();
        }
    }
}
using Common;
using Common.Unit;
using Factories;
using Models;
using Scripts.Common.Unit;
using Scripts.Signals;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UnitInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        public UnitSettings unitSettings;

        public override void InstallBindings()
        {
            BindUnit();
            BindUnitSignals();
            BindUnitServices();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<DamagePopupFactory>().AsSingle().NonLazy();
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
                .WithArguments(unitSettings)
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<AttackService>()
                .AsSingle()
                .WithArguments(unitSettings.unitConfiguration.attackTimeout, unitSettings.unitConfiguration.attackStrength)
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<AnimationService>()
                .AsSingle()
                .WithArguments(unitSettings.spriteRenderer, unitSettings.unitConfiguration.sprite, unitSettings.unitConfiguration.emissionMap)
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<EffectService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUnitSignals()
        {
            Container
                .DeclareSignal<UnitMouseEnterSignal>();
            Container
                .DeclareSignal<UnitMouseExitSignal>();
        }

        private void BindUnit()
        {
            Container
                .BindInterfacesAndSelfTo<UnitModel>()
                .AsSingle()
                .WithArguments(unitSettings)
                .NonLazy();
            Container
                .BindInterfacesTo<UnitInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        public void Initialize()
        {
            var factory = Container.Resolve<DamagePopupFactory>();
            factory.Load();
        }
    }
}
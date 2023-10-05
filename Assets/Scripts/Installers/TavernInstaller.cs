using Common;
using Common.Tavern;
using Factories;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class TavernInstaller : MonoInstaller
    {
        [SerializeField]
        private TavernSettings tavernSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(tavernSettings).AsSingle().NonLazy();
            Container.Bind<PlatformFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TavernUnitMover>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TavernUIController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TavernUnitSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlatformSpawner>().AsSingle().WithArguments(tavernSettings.boardPlatformSettings).NonLazy();
            Container.BindInterfacesAndSelfTo<TavernModel>().AsSingle().NonLazy();
        }
    }
}
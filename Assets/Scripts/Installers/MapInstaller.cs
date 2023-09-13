using Common.Map;
using Models;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MapInstaller : MonoInstaller
    {
        [SerializeField]
        private MapSettings mapSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(mapSettings);
            Container.BindInterfacesAndSelfTo<MapModel>().AsSingle().WithArguments(mapSettings).NonLazy();
        }
    }
}
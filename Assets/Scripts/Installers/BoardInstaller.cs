using System.Collections.Generic;
using Common.Board;
using Contracts;
using Factories;
using Models;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BoardInstaller : MonoInstaller
    {
        [SerializeField]
        private BoardSettings boardSettings;

        public override void InstallBindings()
        {
            BindBoard();
            BindFactory();
        }

        private void BindBoard()
        {
            Container.BindInterfacesAndSelfTo<BoardModel>().AsSingle().WithArguments(boardSettings.transform);
            Container.BindInstance(boardSettings);
        }
        
        private void BindFactory()
        {
            /*Container
                .Bind<UnitFactory>()
                .AsSingle();*/
            
            Container
                .Bind<PlatformFactory>()
                .AsSingle();
        }
    }
}
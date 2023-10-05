using System.Collections.Generic;
using Common;
using Common.Board;
using Common.Tavern;
using Common.Unit;
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
            Container.BindInterfacesAndSelfTo<UnitSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BoardModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlatformSpawner>().AsSingle().WithArguments(boardSettings.boardPlatformSettings).NonLazy();
            Container.BindInstance(boardSettings);
        }
        
        private void BindFactory()
        {
            Container
                .Bind<PlatformFactory>()
                .AsSingle();
        }
    }
}
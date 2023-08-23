using System.ComponentModel;
using Common.Board;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class TavernInstaller : MonoInstaller
    {
        [SerializeField]
        private BoardSettings boardSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TavernModel>().AsSingle().WithArguments(boardSettings).NonLazy();
        }
    }
}
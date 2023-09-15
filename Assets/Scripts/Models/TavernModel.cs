using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Tavern;
using Common.Unit.Hero;
using Controllers;
using Factories;
using UnityEngine;
using Zenject;

namespace Models
{
    public class TavernModel : IInitializable
    {
        [Inject]
        private readonly TavernUnitSpawner _tavernUnitSpawner;

        [Inject]
        private readonly PlatformSpawner _platformSpawner;

        [Inject]
        private readonly TavernSettings _tavernSettings;

        [Inject]
        private readonly TavernUIController _tavernUIController;

        [Inject]
        private LoadingScreenController _loadingScreenController;

        [Inject]
        private MapModel _mapModel;
        
        public List<HeroFacade> Heroes { get; private set; }
        private int _heroesToStart = 3;
        
        public void Initialize()
        {
           _platformSpawner.SpawnPlatforms(false, _tavernSettings.parent); 
           Heroes = _tavernUnitSpawner.SpawnUnits();
           _tavernUIController.OpenMessage();
        }

        public void BeginBattle()
        {
            var heroesSelected = Heroes.Count(x => x.Platform != null);
            if (heroesSelected == _heroesToStart)
            {
                _tavernUIController.CloseMessage();
                _mapModel.Open();
                //_loadingScreenController.Open();
            }
        }
    }
}
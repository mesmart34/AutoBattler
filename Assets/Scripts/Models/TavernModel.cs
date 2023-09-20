using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Board;
using Common.Tavern;
using Common.Unit.Hero;
using UnityEngine;
using Zenject;

namespace Models
{
    public class TavernModel : IInitializable
    {
        private Dictionary<Vector2Int, PlatformFacade> _platforms;
        
        [Inject]
        private readonly TavernUnitSpawner _tavernUnitSpawner;

        [Inject]
        private readonly PlatformSpawner _platformSpawner;

        [Inject]
        private readonly TavernSettings _tavernSettings;

        [Inject]
        private readonly TavernUIController _tavernUIController;
        
        [Inject]
        private MapModel _mapModel;
        
        public List<HeroFacade> Heroes { get; private set; }
        private int _heroesToStart = 3;
        
        public void Initialize()
        {
            _platforms = _platformSpawner.SpawnPlatforms(false, _tavernSettings.parent); 
           Heroes = _tavernUnitSpawner.SpawnTavernUnits(_platforms);
           _tavernUIController.OpenMessage();
           _mapModel.Lock();
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
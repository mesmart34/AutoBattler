using System;
using System.Collections.Generic;
using System.Linq;
using Common.Board;
using Common.Hero;
using Common.Unit.Hero;
using Contracts;
using Factories;
using Models;
using UnityEngine;
using Zenject;

namespace Common.Tavern
{
    public class TavernUnitSpawner : IInitializable
    {
        [Inject]
        private UnitFactory _unitFactory;

        [Inject]
        private TavernSettings _tavernSettings;

        public void Initialize()
        {
            
        }
        
        public List<HeroFacade> SpawnTavernUnits(Dictionary<Vector2Int, PlatformFacade> platforms)
        {
            var heroes = new List<HeroFacade>();
            var unitsOnBoard = _tavernSettings.playerBoardConfiguration.units;
            foreach (var tavernHero in _tavernSettings.heroesToSpawn)
            {
                if (unitsOnBoard.Any(x => x.name == tavernHero.hero.name))
                {
                    var pos = unitsOnBoard.FirstOrDefault(x => x.name == tavernHero.hero.name)?.position;
                    if(pos == null)
                        continue;
                    var platform = platforms[pos.Value];
                    var transform = platform.transform;
                    var unitFacade = _unitFactory.CreateHero(tavernHero.hero, transform.position, _tavernSettings.heroesParent);
                    heroes.Add(unitFacade);   
                    platform.SetUnit(unitFacade);
                }
                else
                {
                    heroes.Add(_unitFactory.CreateHero(tavernHero.hero, tavernHero.spawnPoint.position, _tavernSettings.heroesParent));
                }
            }

            return heroes;
        }
    }
}
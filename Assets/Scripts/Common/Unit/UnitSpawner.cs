using System.Collections.Generic;
using Common.Board;
using Common.Map;
using Common.Unit.Enemy;
using Common.Unit.Hero;
using Factories;
using UnityEngine;
using Zenject;

namespace Common.Unit
{
    public class UnitSpawner
    {
        [Inject]
        private UnitFactory _unitFactory;

        public List<HeroFacade> SpawnHeroes(PlayerBoardConfiguration playerBoardConfiguration, Dictionary<Vector2Int, PlatformFacade> platforms, Transform parent)
        {
            List<HeroFacade> heroes = new();
            foreach (var unit in playerBoardConfiguration.units)
            {
                var platform = platforms[unit.position];
                var heroFacade = _unitFactory.CreateHeroByName(unit.name, platform.transform.position, parent);
                platform.SetUnit(heroFacade);
                heroes.Add(heroFacade);
            }
            return heroes;
        }
        
        public List<EnemyFacade> SpawnEnemies(LevelConfiguration levelConfiguration, Dictionary<Vector2Int, PlatformFacade> platforms, Transform parent)
        {
            List<EnemyFacade> enemies = new();
            foreach (var unit in levelConfiguration.enemies)
            {
                var platform = platforms[unit.position];
                var enemy = _unitFactory.CreateEnemy(unit.enemyConfiguration, platform.transform.position, parent);
                platform.SetUnit(enemy);
                enemies.Add(enemy);
            }
            return enemies;
        }
    }
}
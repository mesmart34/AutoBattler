using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common;
using Common.Board;
using Contracts;
using Controllers;
using DG.Tweening;
using Factories;
using Scripts.Signals;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Models
{
    public class BoardModel : IInitializable
    {
        private bool _battling;
        private Transform _transform;

        public List<UnitFacade> Units { get; private set; } = new();
        public List<UnitFacade> Enemies { get; private set; } = new();
        public List<PlatformFacade> PlayerPlatforms { get; private set; } = new();
        public List<PlatformFacade> EnemyPlatforms { get; private set; } = new();

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private IUnitFactory _unitFactory;

        [Inject]
        private readonly BoardSettings _boardSettings;

        [Inject]
        private readonly PlatformFactory _platformFactory;

        [Inject]
        private readonly PlayerUIController _playerUIController;


        public BoardModel(Transform transform)
        {
            _transform = transform;
        }

        public BoardState BoardState { get; private set; } = BoardState.Tavern;

        public void StartBattle()
        {
            if (_boardSettings.isTavern)
                return;
            setup();
            _battling = true;
            foreach (var enemy in Enemies)
            {
                enemy.StartBattle();
            }

            foreach (var unit in Units)
            {
                unit.StartBattle();
            }
        }

        public UnitFacade FindEnemy(bool nearest = true)
        {
            return nearest
                ? Enemies.OrderBy(x => x.Platform.position.x).FirstOrDefault(x => x.IsAlive)
                : Enemies.OrderByDescending(x => x.Platform.position.x).FirstOrDefault(x => x.IsAlive);
        }

        public UnitFacade FindPlayer(UnitFacade unitFacade, bool nearest = true)
        {
            if (Units.Any(x => x.Platform.IsFront == nearest))
            {
                return Units
                    .OrderBy(x => Vector3.Distance(unitFacade.transform.position, x.transform.position))
                    .Where(x => x.Platform.IsFront == nearest)
                    .FirstOrDefault(x => x.IsAlive);
            }

            return Units
                .OrderBy(x => Vector3.Distance(unitFacade.transform.position, x.transform.position))
                .FirstOrDefault(x => x.IsAlive);
        }

        public UnitFacade GetTarget(UnitFacade unit)
        {
            if (!unit.IsEnemy)
            {
                return Enemies.FirstOrDefault(x => x.IsAlive);
            }

            return Units.FirstOrDefault(x => x.IsAlive);
        }

        private void setup()
        {
            /*_signalBus.Subscribe<UnitPositionChangeSignal>(() =>
            {
                FindTargetsForAllUnits();
            });*/


            var distanceBetweenSides = _boardSettings.distanceBetweenSides;
            PlayerPlatforms = CreateBoardSide(Vector3.left * distanceBetweenSides);
            if (!_boardSettings.isTavern)
            {
                EnemyPlatforms = CreateBoardSide(Vector3.right * distanceBetweenSides, true);
            }
            
            foreach (var playerConf in _boardSettings.playerBoardConfiguration.units)
            {
                var platformFacade = PlayerPlatforms.FirstOrDefault(x => x.position == playerConf.position);
                SpawnPlayerUnit(playerConf.name, platformFacade);
            }
        }

        public void Initialize()
        {
            //_signalBus.Subscribe<UnitDieSignal>(OnSomeUnitDie);
            _unitFactory.Load();
            _platformFactory.Load();
            setup();
            //StartBattle();
            /*SpawnPlayerUnit("Paladin");
            SpawnPlayerUnit("Knight");      
            SpawnPlayerUnit("Forester");
            SpawnPlayerUnit("Bard");
            
            SpawnEnemyUnit("Nigger");
            SpawnEnemyUnit("Fire Worm");
    
            FindTargetsForAllUnits();*/

            //_playerUIController.ShowBeginAutoBattlePanel("Afro");
        }

        /*private void FindTargetsForAllUnits()
        {
            foreach (var unit in _units)
            {
                unit.FindTarget();
            }
            foreach (var unit in _enemies)
            {
                unit.FindTarget();
            }
        }*/
        
        private List<PlatformFacade> CreateBoardSide(Vector3 offset, bool invertSides = false)
        {
            var platforms = new List<PlatformFacade>();
            var spacing = _boardSettings.spacing;
            for (var x = 0; x < _boardSettings.boardPlayerSideSize.x; x++)
            {
                for (var y = 0; y < _boardSettings.boardPlayerSideSize.y; y++)
                {
                    var pos = new Vector3(x * spacing + spacing + offset.x, _boardSettings.position.y,
                        y * spacing + spacing + offset.y);
                    var platform = _platformFactory.Create(new Vector2Int(x, y), pos, invertSides, _transform);
                    if (invertSides)
                    {
                        if (x == 0)
                        {
                            platform.IsFront = true;
                        }
                    }
                    else
                    {
                        if (x == _boardSettings.boardPlayerSideSize.x - 1)
                        {
                            platform.IsFront = true;
                        }
                    }

                    platform._draggable = !invertSides;
                    platforms.Add(platform);
                }
            }

            return platforms;
        }


        private void SpawnPlayerUnit(string prefabName, PlatformFacade platformFacade)
        {
            var spawnPoint = platformFacade!.transform;
            var unitFacade = _unitFactory.Create(prefabName, false, spawnPoint);
            platformFacade.SetUnit(unitFacade);
            Units.Add(unitFacade);
        }

        private void SpawnEnemyUnit(string prefabName, Vector2Int position)
        {
            var platform = EnemyPlatforms
                .FirstOrDefault(x => !x.unitFacade && x.position == position);
            if (platform == null)
            {
                platform = EnemyPlatforms.First();
            }

            var spawnPoint = platform.transform;
            var unitFacade = _unitFactory.Create(prefabName, true, spawnPoint);
            platform.unitFacade = unitFacade;
            unitFacade.Platform = platform;
            Enemies.Add(unitFacade);
        }

        public void OnSomeUnitDie()
        {
            if (!Enemies.Any(x => x.IsAlive))
            {
                _signalBus.Fire<StopBattleSignal>();
                StopBattle(true);
            }

            if (!Units.Any(x => x.IsAlive))
            {
                _signalBus.Fire<StopBattleSignal>();
                StopBattle(false);
            }
        }

        private void StopBattle(bool win)
        {
            if (_battling)
            {
                _playerUIController.ShowBattleEndScreen(win);
                _battling = false;
            }
        }

        private void PrepareUnitsAndEnemies()
        {
            Units.AddRange(PlayerPlatforms.Where(x => x.unitFacade != null).Select(x => x.unitFacade));
            //_enemies.AddRange(EnemyPlatforms.Select(x => x.unitFacade));
            foreach (var enemy in _boardSettings.enemyBoardConfiguration.enemies)
            {
                SpawnEnemyUnit(enemy.name, enemy.position);
            }


            foreach (var unit in Units)
            {
                unit.PrepareMode();
            }

            foreach (var unit in Enemies)
            {
                unit.PrepareMode();
            }
        }


        public void Ready()
        {
            if (_boardSettings.isTavern)
            {
                return;
            }

            BoardState = BoardState.Prepare;
            _playerUIController.ShowBeginAutoBattlePanel(_boardSettings.enemyBoardConfiguration.name);
            PrepareUnitsAndEnemies();
        }

        public void SavePlayerConfiguration()
        {
            _boardSettings.playerBoardConfiguration.units.Clear();
            foreach (var playerPlatform in PlayerPlatforms)
            {
                if (!playerPlatform.Busy)
                {
                    continue;
                }

                _boardSettings.playerBoardConfiguration.units.Add(new UnitSetup()
                {
                    position = playerPlatform.position,
                    name = playerPlatform.unitFacade.Name
                });
            }
        }
    }
}
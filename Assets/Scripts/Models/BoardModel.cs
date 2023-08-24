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
    public class BoardModel : IInitializable, ITickable
    {
        private Transform _transform;
        private List<UnitFacade> _units = new();
        private List<UnitFacade> _enemies = new();
        private List<PlatformFacade> PlayerPlatforms { get; set; } = new();
        private List<PlatformFacade> EnemyPlatforms { get; set; } = new();

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
            foreach (var enemy in _enemies)
            {
                enemy.StartBattle();
            }
            foreach (var unit in _units)
            {
                unit.StartBattle();
            }
        }

        public UnitFacade FindEnemy(bool nearest = true)
        {
            return nearest ? _enemies.OrderBy(x => x.Platform.position.x).FirstOrDefault(x => x.IsAlive) 
                : _enemies.OrderByDescending(x => x.Platform.position.x).FirstOrDefault(x => x.IsAlive);
        }
        
        public UnitFacade FindPlayer(UnitFacade unitFacade, bool nearest = true)
        {
            if (_units.Any(x => x.Platform.IsFront == nearest))
            {
                return _units
                    .OrderBy(x => Vector3.Distance(unitFacade.transform.position, x.transform.position))
                    .Where(x => x.Platform.IsFront == nearest)
                    .FirstOrDefault(x => x.IsAlive);
            }
            return _units
                .OrderBy(x => Vector3.Distance(unitFacade.transform.position, x.transform.position))
                .FirstOrDefault(x => x.IsAlive);
        }
        
        public UnitFacade GetTarget(UnitFacade unit)
        {
            if (!unit.IsEnemy)
            {
                return _enemies.FirstOrDefault(x => x.IsAlive);
            }

            return _units.FirstOrDefault(x => x.IsAlive);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<UnitDieSignal>(OnSomeUnitDie);
            /*_signalBus.Subscribe<UnitPositionChangeSignal>(() =>
            {
                FindTargetsForAllUnits();
            });*/
            
            _unitFactory.Load();
            _platformFactory.Load();

            var distanceBetweenSides = _boardSettings.distanceBetweenSides;
            PlayerPlatforms = CreateBoardSide(Vector3.left * distanceBetweenSides);
            EnemyPlatforms = CreateBoardSide(Vector3.right * distanceBetweenSides, true);

            BoardState = BoardState.Tavern;

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

        public List<PlatformFacade> CreateTavernBoard(Vector3 offset)
        {
            var platforms = new List<PlatformFacade>();
            var spacing = _boardSettings.spacing;
            for (var x = 0; x < _boardSettings.boardPlayerSideSize.x; x++)
            {
                for (var y = 0; y < _boardSettings.boardPlayerSideSize.y; y++)
                {
                    var position = new Vector3(x * spacing + spacing + offset.x, 0, y * spacing + spacing + offset.y);
                    platforms.Add(_platformFactory.Create(new Vector2Int(x, y), position  + _boardSettings.position, true, _transform));
                }
            }
            return platforms;
        }
        
        private List<PlatformFacade> CreateBoardSide(Vector3 offset, bool invertSides = false)
        {
            var platforms = new List<PlatformFacade>();
            var spacing = _boardSettings.spacing;
            for (var x = 0; x < _boardSettings.boardPlayerSideSize.x; x++)
            {
                for (var y = 0; y < _boardSettings.boardPlayerSideSize.y; y++)
                {
                    var position = new Vector3(x * spacing + spacing + offset.x, 0, y * spacing + spacing + offset.y);
                    var platform = _platformFactory.Create(new Vector2Int(x, y), position, invertSides, _transform);
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
        

        private void SpawnPlayerUnit(string prefabName)
        {
            var platform = PlayerPlatforms.FirstOrDefault(x => !x.unitFacade);
            var spawnPoint = platform!.transform;
            var unitFacade = _unitFactory.Create(prefabName, false, spawnPoint);
            platform.unitFacade = unitFacade;
            unitFacade.Platform = platform;
            _units.Add(unitFacade);
        }
        
        private void SpawnEnemyUnit(string prefabName)
        {
            var platform = EnemyPlatforms.FirstOrDefault(x => !x.unitFacade);
            var spawnPoint = platform!.transform;
            var unitFacade = _unitFactory.Create(prefabName, true, spawnPoint);
            platform.unitFacade = unitFacade;
            unitFacade.Platform = platform;
            _enemies.Add(unitFacade);
        }

        private void OnSomeUnitDie()
        {
            if (!_enemies.Any(x => x.IsAlive))
            {
                Debug.Log("Player won");
                _signalBus.Fire<StopBattleSignal>();
                StopBattle(true);
            }
            if (!_units.Any(x => x.IsAlive))
            {
                Debug.Log("Player fucked up");
                _signalBus.Fire<StopBattleSignal>();
                StopBattle(false);
            }
        }

        private void StopBattle(bool win)
        {
            _playerUIController.ShowBattleEndScreen(win);
        }

        public void Tick()
        {
            
        }

        private void PrepareUnitsAndEnemies()
        {
            _units.AddRange(PlayerPlatforms.Where(x => x.unitFacade != null).Select(x => x.unitFacade));
            //_enemies.AddRange(EnemyPlatforms.Select(x => x.unitFacade));
            SpawnEnemyUnit("Fire Worm");
            
            
            foreach (var unit in _units)
            {
                unit.PrepareMode();
            }
            foreach (var unit in _enemies)
            {
                unit.PrepareMode();
            }
        }

        
        public void Ready()
        {
            _boardSettings.camera.transform.DOMove(_boardSettings.cameraBoardPosition.position, 1.0f).onComplete = () =>
            {
                BoardState = BoardState.Prepare;
                _playerUIController.ShowBeginAutoBattlePanel("Fire Worm");
                PrepareUnitsAndEnemies();
            };
        }
    }
}
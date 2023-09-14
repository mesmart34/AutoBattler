using System;
using System.Collections.Generic;
using System.Linq;
using Common.Board;
using Contracts;
using Factories;
using Models;
using UnityEngine;
using Zenject;

namespace Common.Tavern
{
    public class TavernUnitSpawner : MonoBehaviour
    {
        [SerializeField]
        private PlayerBoardConfiguration _playerBoardConfiguration;

        [SerializeField]
        private List<TavernUnitWrapper> _unitsToSpawn = new ();
        
        [Inject]
        private IUnitFactory _unitFactory;
        
        private void Start()
        {
            var units = _unitsToSpawn.Where(x => !_playerBoardConfiguration.units.Any(y => y.name == x.name));
            foreach (var unit in units)
            {
                _unitFactory.Create(unit.name, false, unit.spawnPointTransform);
            }
        }

    }

    [Serializable]
    public class TavernUnitWrapper
    {
        public string name;
        public Transform spawnPointTransform;
    }
}
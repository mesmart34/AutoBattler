using System.Linq;
using Common;
using Contracts;
using Installers;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class UnitFactory : IUnitFactory
    {
        private readonly DiContainer _diContainer;

        private Object[] _unitPrefabs;
        private Object[] _enemyPrefabs;

        public UnitFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _unitPrefabs = Resources.LoadAll("Prefabs/Units");
            _enemyPrefabs = Resources.LoadAll("Prefabs/Enemies");
        }


        private GameObject CreateUnit(string prefabName, bool enemy)
        {
            if (enemy)
            {
                return _enemyPrefabs.Cast<GameObject>().FirstOrDefault(x => x.name == prefabName);    
            }
            return _unitPrefabs.Cast<GameObject>().FirstOrDefault(x => x.name == prefabName);
        }

        public UnitFacade Create(string prefabName, bool enemy, Transform boardPlatform)
        {
            var prefab = CreateUnit(prefabName, enemy);
            var unit = _diContainer.InstantiatePrefabForComponent<UnitFacade>(prefab);
            unit.transform.position = boardPlatform.transform.position;
            return unit;
        }
    }
}
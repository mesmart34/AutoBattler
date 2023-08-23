using Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EffectFactory : PlaceholderFactory<IEffect>
    {
        private readonly DiContainer _diContainer;

        private Object _prefab;

        public EffectFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _prefab = Resources.Load("Prefabs/Unit");
        }
        public void Create(string name, Transform effectBar)
        {
            var gameObject = _diContainer.InstantiatePrefab(_prefab, Vector3.zero, Quaternion.identity, effectBar);
            //var configuration = _units.FirstOrDefault(x => x.name == name);
            //gameObject.GetComponent<UnitInstaller>().unitSettings.unitConfiguration = configuration;
        }
    }
}
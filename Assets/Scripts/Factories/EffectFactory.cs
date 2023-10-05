using Common;
using Common.Effects;
using Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EffectFactory
    {
        private readonly DiContainer _diContainer;

        private Object _prefab;

        public EffectFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _prefab = Resources.Load("Prefabs/Effect");
        }
        
        public GameObject Create(Transform effectBar)
        {
            var effect = _diContainer.InstantiatePrefab(_prefab, Vector3.zero, Quaternion.identity, effectBar);
            var rect = effect.GetComponent<RectTransform>();
            rect.SetParent(effectBar);
            rect.anchoredPosition3D = Vector3.zero;
            return effect;
        }
    }
}
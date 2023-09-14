using Common.Board;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class MapIconFactory
    {
        private readonly DiContainer _diContainer;

        private Object _prefab;

        public MapIconFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _prefab = Resources.Load("Prefabs/MapButton");
        }
        
        public GameObject Create(EnemyBoardConfiguration boardConfiguration, Transform parent)
        {
            var effect = _diContainer.InstantiatePrefab(_prefab, Vector3.zero, Quaternion.identity, parent);
            var rect = effect.GetComponent<RectTransform>();
            rect.anchoredPosition3D = Vector3.zero;
            return effect;
        }
    }
}
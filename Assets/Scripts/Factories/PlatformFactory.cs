using Common.Board;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class PlatformFactory
    {
        private readonly DiContainer _diContainer;
        private Object _prefab;
        
        public PlatformFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            Load();
        }

        public void Load()
        {
            _prefab = Resources.Load("Prefabs/Platform");
        }
        
        public PlatformFacade Create(Vector2Int logicPosition, Vector3 position, bool draggable, Transform parent = null)
        {
            var platformFacade = _diContainer.InstantiatePrefabForComponent<PlatformFacade>(_prefab, Vector3.zero, Quaternion.identity, parent);
            platformFacade.transform.localPosition = position;
            platformFacade.Setup(logicPosition, draggable);
            return platformFacade;
        }
    }
}
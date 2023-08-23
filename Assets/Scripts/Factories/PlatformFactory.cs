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
        }

        public void Load()
        {
            _prefab = Resources.Load("Prefabs/Platform");
        }
        
        public PlatformFacade Create(Vector2Int logicPosition, Vector3 position, bool draggable, Transform parent = null)
        {
            var platformFacade = _diContainer.InstantiatePrefabForComponent<PlatformFacade>(_prefab, position, Quaternion.identity, parent);
            platformFacade.Setup(logicPosition, draggable);
            return platformFacade;
        }
    }
}
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Autobattler/Board Settings")]
    public class BoardPlatformSettings : ScriptableObject
    {
        public Vector3 position;
        public Vector2Int boardPlayerSideSize;
        public Vector2Int size;
        public float spacing = 10.0f;
        public float distanceBetweenSides = 1;
    }
}
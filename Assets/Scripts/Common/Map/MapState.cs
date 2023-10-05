using System.Collections.Generic;
using Common.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Map
{
    [CreateAssetMenu(menuName = "Autobattler/Map State", fileName = "Map State")]
    public class MapState : ScriptableObject
    {
        public List<MapColumnWrapper> level;
    }
}
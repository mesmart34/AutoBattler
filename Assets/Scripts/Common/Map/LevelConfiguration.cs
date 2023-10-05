using System.Collections.Generic;
using Common.Enemy;
using UnityEngine;

namespace Common.Map
{
    [CreateAssetMenu(menuName = "Autobattler/Level Configuration", fileName = "Level Configuration")]
    public class LevelConfiguration : ScriptableObject
    {
        public string name = "Unknown";
        public List<MapEnemyWrapper> enemies;
    }
}
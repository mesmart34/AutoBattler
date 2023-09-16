using System;
using Common.Enemy;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class MapEnemyWrapper
    {
        public EnemyConfiguration enemyConfiguration;
        public Vector2Int position;
    }
}
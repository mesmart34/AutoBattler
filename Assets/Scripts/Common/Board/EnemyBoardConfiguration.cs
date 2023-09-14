using System;
using System.Collections.Generic;
using Scripts.Common.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Board
{
    [CreateAssetMenu(menuName = "Autobattler/Enemy Board Configuration", fileName = "Enemy Board Configuration")]
    public class EnemyBoardConfiguration : ScriptableObject
    {
        public string name;
        public List<UnitSetup> enemies;
    }

    [Serializable]
    public class UnitSetup
    {
        public string name;
        public Vector2Int position;
    }
}
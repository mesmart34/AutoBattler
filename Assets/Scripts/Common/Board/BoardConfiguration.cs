using System;
using System.Collections.Generic;
using Scripts.Common.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Board
{
    [CreateAssetMenu(menuName = "Autobattler/Board Configuration", fileName = "Board Configuration")]
    public class BoardConfiguration : ScriptableObject
    {
        public string name;
        public List<UnitSetup> enemies;
    }

    [Serializable]
    public class UnitSetup
    {
        public string enemyPrefabName;
        public Vector2Int position;
    }
}
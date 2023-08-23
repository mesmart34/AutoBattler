using System.Collections.Generic;
using Scripts.Common.Unit;
using UnityEngine;

namespace Common.Board
{
    [CreateAssetMenu(menuName = "Autobattler/Board Setup")]
    public class BoardUnitSetup : ScriptableObject
    {
        public Dictionary<Vector2Int, UnitConfiguration> Setup;
    }
}
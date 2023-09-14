using System.Collections.Generic;
using UnityEngine;

namespace Common.Board
{
    [CreateAssetMenu(menuName = "Autobattler/Player Board Configuration", fileName = "Player Board Configuration")]
    public class PlayerBoardConfiguration : ScriptableObject
    {
        public List<UnitSetup> units;
    }
}
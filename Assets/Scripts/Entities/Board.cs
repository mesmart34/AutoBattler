using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    public class Board : MonoBehaviour
    {
        public static Board Instance;
        public List<Unit> Units;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public Unit GetTarget(Unit unit)
        {
            if (!unit.IsEnemy)
            {
                return Units.FirstOrDefault(x => x.IsEnemy && x._healthController.Health > 0);
            }
            else
            {
                return Units.FirstOrDefault(x => !x.IsEnemy && x._healthController.Health > 0);
            }
                
        }

        public List<Unit> GetUnits(Predicate<Unit> predicate)
        {
            return Units.Where(x => predicate(x)).ToList();
        }
    }
}
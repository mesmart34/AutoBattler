using System.Linq;
using Contracts;
using AutoBattler;
using UnityEngine;

namespace ScriptableObjects.Skills
{
    [CreateAssetMenu]
    public class HolyShield : SkillData
    {
        private Unit _unit;
        
        public override void OnInitialize(Unit unit)
        {
            unit._manaController.OnManaRestored += OnManaRestored;
        }

        private void OnManaRestored()
        {
            Board.Instance.GetUnits(x => x.IsEnemy == _unit.IsEnemy && _unit.Name != x.Name)
                .Select(x => x._physicalShieldComponent.Shield += 20 + 3 * _unit._healthController.PhysicalResist);
            _unit._manaController.Mana = 0;
        }
    }
}
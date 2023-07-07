using AutoBattler;
using Contracts;
using UnityEngine;

namespace Scriptable_Objects.Skills
{
    [CreateAssetMenu]
    public class SoreSpot : SkillData
    {
        [Min(0)]
        public int StealManaAmount = 5;
        [Min(0)]
        public int ResistIgnoreAmount = 5;
        
        private Unit _unit;
        
        public override void OnInitialize(Unit unit)
        {
            _unit = unit;
            unit._attackController.OnAttackPrepare += OnAttackPrepare;
            unit._attackController.OnAttacked += OnAttacked;
        }

        private void OnAttackPrepare(Damage damage, Unit target)
        {
            damage.PhysicalResistIgnoreAmount = ResistIgnoreAmount;
        }

        public void OnAttacked(Unit unit)
        {
            unit._manaController.Mana -= StealManaAmount;
            _unit._manaController.Mana += StealManaAmount;
        }
    }
}
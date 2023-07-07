using System.Linq;
using AutoBattler;
using Contracts;
using UnityEngine;

namespace Scriptable_Objects.Skills
{
    [CreateAssetMenu]
    public class DoubleShoot : SkillData
    {
        private Unit _unit;
        public override void OnInitialize(Unit unit)
        {
            _unit = unit;
            _unit._attackController.OnAttacked += OnAttacked;
        }

        private void OnAttacked(Unit target)
        {
            var newTarget = Board.Instance.GetUnits(x => x.IsEnemy == target.IsEnemy && x.Name != target.Name).FirstOrDefault();
            if (newTarget == null)
                return;
            
            newTarget._damageController.ApplyDamage(new Damage()
            {
                Amount = _unit._attackController.Strength / 2,
                Type = DamageType.Physical
            });
        }
    }
}
using System;
using Contracts;
using AutoBattler;
using UnityEngine;

namespace ScriptableObjects.Skills
{
    [CreateAssetMenu]
    public class Weakness : SkillData
    {
        public int DamageAmount = 100;
        public float X = 1.0f;
        private Unit _unit;
        
        public override void OnInitialize(Unit unit)
        {
            _unit = unit;
            unit._manaController.OnManaRestored += OnManaRestored;
        }

        private void OnManaRestored()
        {
            var enemies = Board.Instance.GetUnits(x => x.IsEnemy != _unit.IsEnemy);
            foreach (var enemy in enemies)
            {
                if (enemy._physicalShieldComponent.Shield == 0)
                {
                    enemy._healthController.Health -= DamageAmount / 2;
                    continue;
                }
                enemy._physicalShieldComponent.Shield -= DamageAmount;
                if (enemy._physicalShieldComponent.Shield == 0)
                {
                    enemy._healthController.Health -= (int)(DamageAmount * X);
                }
                else
                {
                    enemy._damageController.ApplyDamage(new Damage()
                    {
                        Amount = 50,
                        Type = DamageType.Physical,
                        DamageIgnore = DamageIgnore.PhysicalShield
                    });
                }
            }
            _unit._manaController.Mana = 0;
        }
    }
}
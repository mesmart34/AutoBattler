using System;
using System.Collections;
using AutoBattler;
using Unity.VisualScripting;
using UnityEngine;
using Unit = AutoBattler.Unit;

namespace Effects
{
    [CreateAssetMenu]
    public class BurningEffectData : EffectData
    {
        public readonly float ExistTime;
        public readonly float Cooldown;
        public readonly int DamageAmount;
        public DamageType DamageType;

        public override void OnInitialize(Unit unit)
        {
            unit.StartCoroutine(Coroutine(unit));
        }
        
        public IEnumerator Coroutine(Unit unit)
        {
            while (unit._healthController.Health > 0)
            {
                unit._healthController.Health -= DamageAmount;
                yield return new WaitForSeconds(Cooldown);
            }
        }
    }
}
using System;
using System.Collections;
using AutoBattler;
using UnityEngine;

namespace Scriptable_Objects.Effects
{
    [CreateAssetMenu]
    public class PoisonEffect : EffectData
    {
        public float ExistTime = 15;
        public float Cooldown;
        public int DamageAmount;

        public override void OnInitialize(Unit unit)
        {
            unit.StartCoroutine(ExistCoroutine(unit));
            unit.StartCoroutine(DamageCoroutine(unit));
        } 

        private IEnumerator ExistCoroutine(Unit unit)
        {
            unit._attackController.MissProbability += 0.02f;
            var time = 0.0f;
            while (time < ExistTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            unit._attackController.MissProbability -= 0.02f;
            unit.StopCoroutine(DamageCoroutine(unit));
        }
        
        private IEnumerator DamageCoroutine(Unit unit)
        {
            while (true)
            {
                unit._healthController.Health -= DamageAmount;
                yield return new WaitForSeconds(Cooldown);
            }
        }
    }
}
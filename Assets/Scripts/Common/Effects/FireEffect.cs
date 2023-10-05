using System;
using System.Collections;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Effects
{
    [CreateAssetMenu(menuName = "Autobattler/Effects/Fire", fileName = "FireEffect")]
    public class FireEffect : EffectScriptable
    {
        public int damageAmount = 4;
        public float speed = 2.0f;
        public DamageType damageType = DamageType.Magical;
        
        public override IEnumerator DoLogic(UnitModel owner, BoardModel board, Action onInitialize, Action onTick, Action onDispose)
        {
            var timer = 0.0f;
            var attackTimer = 0.0f;
            onInitialize?.Invoke();
            while (timer <= effectSettings.TimeExist)
            {
                timer += Time.deltaTime;
                attackTimer += Time.deltaTime;
                if (attackTimer >= speed)
                {
                    attackTimer = 0.0f;
                    owner.ApplyDamageByEffect(damageAmount, damageType);
                    onTick?.Invoke();
                }
                yield return null;
            }
            onDispose?.Invoke();
            yield return null;
        }
    }
}
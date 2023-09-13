using System;
using System.Collections;
using Models;
using UnityEngine;

namespace Common.Effects
{
    [CreateAssetMenu(menuName = "Autobattler/Effects/Silent", fileName = "Silent")]
    public class Silent : EffectScriptable
    {
        public override void Sum(EffectModel original, EffectModel other)
        {
            original.EffectSettings.TimeExist += other.EffectSettings.TimeExist;
            Debug.Log(original.EffectSettings.TimeExist);
        }

        public override IEnumerator DoLogic(UnitModel owner, BoardModel board, Action onInitialize, Action onTick,
            Action onDispose)
        {
            onInitialize?.Invoke();
            owner.SetManaServiceActive(false);
            yield return new WaitForSeconds(effectSettings.TimeExist);
            owner.SetManaServiceActive(true);
            onDispose?.Invoke();
        }
    }
}
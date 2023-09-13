using System;
using System.Collections;
using Common.Effects;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    public class EffectScriptable : ScriptableObject
    {
        public EffectSettings effectSettings;

        public virtual void Sum(EffectModel original, EffectModel other)
        {
            
        }
        
        public virtual IEnumerator DoLogic(UnitModel owner, BoardModel board, Action onInitialize, Action onTick, Action onDispose)
        {
            yield return null;
        }
    }
}
using Contracts;
using AutoBattler;
using Effects;
using UnityEngine;

namespace ScriptableObjects.Skills
{
    [CreateAssetMenu]
    public class GodHand : SkillData
    {
        public override void OnInitialize(Unit unit)
        {
            unit._attackController.OnAttacked += OnAttacked;
        }

        private void OnAttacked(Unit target)
        {
            target._effectsController.AddEffect(CreateInstance<BurningEffectData>());
        }
    }
}
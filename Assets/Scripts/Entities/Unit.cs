using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoBattler
{
    public class Unit : MonoBehaviour
    {
        [FormerlySerializedAs("unitConfiguration")]
        [SerializeField]
        private UnitData unitData;
        
        [HideInInspector]
        public HealthController _healthController;
        [HideInInspector]
        public ManaController _manaController;
        [HideInInspector]
        public ShieldController _physicalShieldComponent;
        [HideInInspector]
        public ShieldController _magicalShieldComponent;
        [HideInInspector]
        public AttackController _attackController;
        [HideInInspector]
        public DamageController _damageController;
        [HideInInspector]
        public EffectsController _effectsController;
        [HideInInspector]
        public SkillController _skillController;

        public string Name
        {
            get => unitData.Name;
        }

        public bool IsEnemy
        {
            get => unitData.IsEnemy;
        }

        private void Awake()
        {
            Init();
        }

        private void OnDied()
        {
            StopAllCoroutines();
        }
        
        private void Init()
        {
            _healthController = gameObject.AddComponent<HealthController>();
            _healthController.Health = unitData.Health;
            _healthController.MagicalResist = unitData.PhysicalDamageResist;
            _healthController.MagicalResist = unitData.MagicalDamageResist;
            _healthController.OnDied += OnDied;

            _physicalShieldComponent = gameObject.AddComponent<ShieldController>();
            _physicalShieldComponent.Shield = unitData.PhysicalShield;
            
            _magicalShieldComponent = gameObject.AddComponent<ShieldController>();
            _magicalShieldComponent.Shield = unitData.MagicalShield;

            _attackController = gameObject.AddComponent<AttackController>();
            _attackController.DamageType = unitData.DamageType;
            _attackController.Cooldown = unitData.AttackRecoverTime;
            _attackController.Strength = unitData.AttackStrength;
            _attackController.MissProbability = unitData.AttackMissProbabilty;

            _damageController = gameObject.AddComponent<DamageController>();
            _effectsController = gameObject.AddComponent<EffectsController>();

            _skillController = gameObject.AddComponent<SkillController>();
            _skillController._skills = unitData.Skills;
            
            _manaController = gameObject.AddComponent<ManaController>();
            _manaController.Mana = unitData.Mana;
            _manaController.RegenerationAmountPerTick = unitData.ManaRegenerationAmountPerTick;
        }
        
    }
}
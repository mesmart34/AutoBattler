using System;
using System.Collections;
using System.Collections.Generic;
using Contracts;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoBattler
{
    [CreateAssetMenu]
    public class UnitData : ScriptableObject
    {
        public string Name
        {
            get => _name;
        }

        public int Health
        {
            get => _health;
        }

        public int AttackStrength
        {
            get => _attackStrength;
        }

        public float AttackRecoverTime
        {
            get => _attackRecoverTime;
        }
        public float AttackMissProbabilty
        {
            get => _attackMissProbability;
        }
        public DamageType DamageType
        {
            get => _attackDamageType;
        }

        public int PhysicalDamageResist
        {
            get => _physicalDamageResist;
        }
        
        public int PhysicalShield
        {
            get => _physicalShield;
        }
        
        public int MagicalShield
        {
            get => _magicalShield;
        }

        public int MagicalDamageResist
        {
            get => _magicalDamageResist;
        }

        public List<SkillData> Skills
        {
            get => _skills;
        }

        public bool IsEnemy
        {
            get => _isEnemy;
        }

        public int Mana
        {
            get => _mana;
        }

        public int ManaRegenerationAmountPerTick
        {
            get => _manaRegenerationAmountPerTick;
        }

        [SerializeField]
        private string _name;
        [SerializeField]
        private int _health;
        [SerializeField]
        private int _mana;
        [SerializeField]
        private int _manaRegenerationAmountPerTick;
        [SerializeField]
        private int _attackStrength;
        [SerializeField]
        private float _attackRecoverTime;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _attackMissProbability;
        [SerializeField]
        private DamageType _attackDamageType;
        [SerializeField]
        private int _physicalShield;
        [SerializeField]
        private int _physicalDamageResist;
        [SerializeField]
        private int _magicalShield;
        [SerializeField]
        private int _magicalDamageResist;
        [SerializeField]
        private bool _isEnemy;
        [SerializeField]
        private List<SkillData> _skills;
    }
}
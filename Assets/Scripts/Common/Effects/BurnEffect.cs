using System;
using Code.Unit;
using Models;
using Scripts.Contracts;
using UnityEngine;

namespace Scripts.Common.Effects
{
    public class BurnEffect : IEffect
    {
        private float _timer;
        private float _attackTimer;
        private int _attackStrength = 4;
        private float _timeExist = 15;
        private float _attackCooldown = 2;

        public string Name { get; set; } = "Burn";
        public string IconPath { get; set; } = "Effects/fire";
        public event Action<IEffect> OnEffectCooldown;
        public event Action<IEffect> OnEffectAdd;
        public event Action<IEffect> OnEffectRemove;

        public void Initialize(UnitModel model)
        {
            OnEffectAdd?.Invoke(this);
        }

        public void Tick(UnitModel model)
        {
            _timer += Time.deltaTime;
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackCooldown)
            {
                //_healthService.ApplyDamage(attackStrength);
                model.ApplyDamage(_attackStrength);
                OnEffectCooldown?.Invoke(this);
                _attackTimer = 0;
            }
            if (_timer >= _timeExist)
            {
                OnEffectRemove?.Invoke(this);
            }
        }

        public void Dispose(UnitModel model)
        {
            OnEffectRemove?.Invoke(this);
        }
    }
}
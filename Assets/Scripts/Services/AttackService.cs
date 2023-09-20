using System;
using Common.Unit;
using Models;
using UnityEngine;
using Zenject;

namespace Services
{
    public class AttackService : IInitializable, ITickable
    {
        private readonly UnitModel _unitModel;
        private float _attackTimeout;
        private int _attackStrength;
        private bool _running;
        private float _time;
        private UnitFacade _target;

        [Inject]
        private AnimationService _animationService;

        public Action<float, float> OnAttackTimerChange;
        public Action OnTargetNeed;

        public UnitFacade Target => _target;
        
        public void Activate(UnitData unitData)
        {
            _attackTimeout = unitData.attackTimeout;
            _attackStrength = unitData.attackStrength;
            _running = true;
        }

        public void Deactivate()
        {
            _running = false;
        }
        
        private void Attack()
        {
            if (_target == null)
            {
                return;
            }
            
            _animationService.PlayAttackAnimation();
            _target.ApplyDamage(_attackStrength);
            
        }

        public void SetTarget(UnitFacade unitFacade)
        {
            _target = unitFacade;
        }

        public void Initialize()
        {
            
        }
        
        public void Tick()
        {
            if (!_running)
            {
                return;
            }

            _time += Time.deltaTime;
            OnAttackTimerChange?.Invoke(_time, _attackTimeout);
            if (_time < _attackTimeout)
            {
                return;
            }
            _time = 0;

            if (_target == null)
            {
                OnTargetNeed?.Invoke();
            }
            
            Attack();
        }
    }
}
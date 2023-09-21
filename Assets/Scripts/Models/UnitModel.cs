using System;
using Code.Unit;
using Common;
using Common.Unit;
using Services;
using UnityEngine;
using Zenject;

namespace Models
{
    [Serializable]
    public class UnitModel : IInitializable
    {
        private Canvas _canvas;
        private GameObject _bars;
        public UnitData UnitData { get; private set; }
        public bool IsAlive => _healthService.IsAlive;

        [Inject]
        protected UnitSettings UnitSettings;

        [Inject]
        private AnimationService _animationService;
        
        [Inject]
        private AttackService _attackService;
        
        [Inject]
        private ManaService _manaService;
        
        [InjectOptional]
        private BoardModel _boardModel;

        [Inject]
        private HealthService _healthService;

        [Inject]
        private UnitFacade _unitFacade;

        public void SetupWithUnitData(UnitData unitData)
        {
            UnitData = unitData;
            _canvas = UnitSettings.canvas;
            _bars = UnitSettings.bars;
            _animationService.Activate(UnitData);
            _healthService.SetMaxHealth(UnitData.health);
            if (UnitData.locked)
            {
                _animationService.PlayLockedAnimation();
            }
        }

        public void Initialize()
        {
            _healthService.OnHealthValueChanged += OnHealthChanged;
            _attackService.OnTargetNeed += OnTargetDestroyed;
        }

        private void OnTargetDestroyed()
        {
            var target = _boardModel.FindTarget(_unitFacade);
            _attackService.SetTarget(target);
        }

        private void Die()
        {
            _animationService.PlayDeadAnimation();
            Deactivate();
        }

        public void SetAttackServiceActive(bool value)
        {
            
        }
        
        public void SetManaServiceActive(bool value)
        {
            if (value)
            {
                _manaService.Activate(UnitData);
            }
            else
            {
                _manaService.Deactivate();
            }
        }

        public void SetBarsActive(bool value)
        {
            _bars.SetActive(value);
        }

        public void ApplyDamage(int damageAmount, DamageType damageType = DamageType.Physical)
        {
            _healthService?.ApplyDamage(damageAmount);
            _animationService?.PlayRecieveDamageAnimation();
            //_damagePopupFactory.Create(damageAmount, Color.white, _canvas, 5.0f);
        }

        public void ApplyDamageByEffect(int damageAmount, DamageType damageType = DamageType.Physical)
        {
            _healthService?.ApplyDamage(damageAmount);
            _animationService?.PlayRecieveDamageAnimation();
            //_damagePopupFactory.Create(damageAmount, Color.white, _canvas, 5.0f);
        }


        private void OnHealthChanged(int value, int maxValue)
        {
            if (value == 0)
            {
                SetBarsActive(false);
                Die();
            }
        }

        public void Activate()
        {
            _attackService.Activate(UnitData);
            _manaService.Activate(UnitData);
        }

        public void Deactivate()
        {
            _attackService.Deactivate();
            _manaService.Deactivate();
            _animationService.Deactivate();
        }

        public void FindTarget()
        {
            var target = _boardModel.FindTarget(_unitFacade);
            if (_attackService.Target != null && !_attackService.Target.IsAlive)
            {
                _attackService.SetTarget(target);
            }
        }
    }
}
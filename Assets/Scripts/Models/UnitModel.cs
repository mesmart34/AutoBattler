using System;
using System.Collections.Generic;
using Code.Unit;
using Common;
using Common.Unit;
using Contracts;
using Controllers;
using Factories;
using Services;
using Signals;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;
using Random = UnityEngine.Random;

namespace Models
{
    [Serializable]
    public class UnitModel : IInitializable
    {
        private Canvas _canvas;
        private GameObject _bars;
        public UnitData UnitData { get; private set; }

        [Inject]
        protected UnitSettings UnitSettings;
        
        /*
        [Inject]
        private BoardModel _boardModel;

        [Inject]
        private readonly HealthService _healthService;
        
        [Inject]
        private readonly ManaService _manaService;
        
        [Inject]
        private readonly AttackService _attackService;
        
        [Inject]
        private readonly AnimationService _animationService;
        
        [Inject]
        private readonly UnitFacade _unitFacade;

        [Inject]
        private readonly DamagePopupFactory _damagePopupFactory;

        [Inject]
        private EffectController _effectController;*/

        public void SetupWithUnitData(UnitData unitData)
        {
            UnitData = unitData;
        }

        public UnitState UnitState { get; set; } = UnitState.Tavern;

        public bool FindNearestTarget { get; set; }
        

        private void Die()
        {
            /*_boardModel.OnSomeUnitDie();
            _signalBus.Fire<UnitDieSignal>();
            _unitFacade.Die();
            TurnOff();*/
        }

        public void SetManaServiceActive(bool value)
        {
            /*if (value)
            {
                _manaService.TurnOn();
            }
            else
            {
                _manaService.TurnOff();
            }*/
        }

        public void ApplyDamage(int damageAmount, DamageType damageType = DamageType.Physical)
        {
            /*_healthService?.ApplyDamage(damageAmount);
            _animationService?.PlayRecieveDamageAnimation();
            _damagePopupFactory.Create(damageAmount, Color.white, _canvas, 5.0f);*/
        }

        public void ApplyDamageByEffect(int damageAmount, DamageType damageType = DamageType.Physical)
        {
            /*_healthService?.ApplyDamage(damageAmount);
            //_animationService?.PlayRecieveDamageAnimation();
            _damagePopupFactory.Create(damageAmount, Color.white, _canvas, 5.0f);*/
        }

        public virtual void Initialize()
        {
            _canvas = UnitSettings.canvas;
            _bars = UnitSettings.bars;
            /*_spriteRenderer.sprite = _unitConfiguration.sprite[0];
            _healthService.SetMaxHealth(_unitConfiguration.health);
            _healthService.OnHealthValueChanged += OnHealthChanged;
            _bars.SetActive(false);*/
        }

        private void OnHealthChanged(int value, int maxValue)
        {
            /*if (value == 0)
            {
                Die();
            }*/
        }

        private void TurnOff()
        {
            /*_manaService.TurnOff();
            _attackService.TurnOff();
            _animationService.TurnOff();
            _effectController.Deactivate();*/
        }

        public void PrepareMode()
        {
            /*UnitState = UnitState.Idle;
            _attackService.FindTarget();
            _bars.SetActive(true);*/
        }

        public void StartBattle()
        {
            /*_manaService.TurnOn();
            _attackService.TurnOn();
            _effectController.Activate();*/
        }
    }
}
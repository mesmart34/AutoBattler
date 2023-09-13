using System;
using System.Collections.Generic;
using Code.Unit;
using Common;
using Common.Unit;
using Controllers;
using Factories;
using Scripts.Common.Unit;
using Services;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Models
{
    [Serializable]
    public class UnitModel : IInitializable
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly UnitConfiguration _unitConfiguration;
        private readonly Canvas _canvas;
        private GameObject _bars;


        [Inject]
        private readonly SignalBus _signalBus;
        
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
        private EffectController _effectController;

        public UnitModel(UnitSettings unitSettings)
        {
            _unitConfiguration = unitSettings.unitConfiguration;
            _spriteRenderer = unitSettings.spriteRenderer;
            _spriteRenderer.sprite = unitSettings.unitConfiguration.sprite[0];
            _canvas = unitSettings.canvas;
            _bars = unitSettings.bars;
        }
        
        public UnitState UnitState { get; set; } = UnitState.Tavern;
        public bool IsEnemy => _unitConfiguration.isEnemy;

        public bool IsAlive => _healthService.IsAlive;

        public bool FindNearestTarget { get; set; }
        
        public string Name => _unitConfiguration.name;

        private void Die()
        {
            _signalBus.Fire<UnitDieSignal>();
            _unitFacade.Die();
            TurnOff();
        }

        public void SetManaServiceActive(bool value)
        {
            if (value)
            {
                _manaService.TurnOn();
            }
            else
            {
                _manaService.TurnOff();
            }
        }

        public void ApplyDamage(int damageAmount, DamageType damageType = DamageType.Physical)
        {
            _healthService?.ApplyDamage(damageAmount);
            _animationService?.PlayRecieveDamageAnimation();
            _damagePopupFactory.Create(damageAmount, Color.white, _canvas, 5.0f);
        }

        public void ApplyDamageByEffect(int damageAmount, DamageType damageType = DamageType.Physical)
        {
            _healthService?.ApplyDamage(damageAmount);
            //_animationService?.PlayRecieveDamageAnimation();
            _damagePopupFactory.Create(damageAmount, Color.white, _canvas, 5.0f);
        }

        public void Initialize()
        {
            _spriteRenderer.sprite = _unitConfiguration.sprite[0];
            _healthService.SetMaxHealth(_unitConfiguration.health);
            _healthService.OnHealthValueChanged += OnHealthChanged;
            _bars.SetActive(false);
            /*_effectService.AddEffect(new BurnEffect());
            effectController.AddEffect();*/
           // _signalBus.Subscribe<StartBattleSignal>(StartBattle);
            //_signalBus.Subscribe<StopBattleSignal>(TurnOff);
        }

        private void OnHealthChanged(int value, int maxValue)
        {
            if (value == 0)
            {
                Die();
            }
        }

        private void TurnOff()
        {
            _manaService.TurnOff();
            _attackService.TurnOff();
            _animationService.TurnOff();
            _effectController.Deactivate();
        }

        public void PrepareMode()
        {
            UnitState = UnitState.Idle;
            _attackService.FindTarget();
            _bars.SetActive(true);
        }

        public void StartBattle()
        {
            _manaService.TurnOn();
            _attackService.TurnOn();
            _effectController.Activate();
        }
    }
}
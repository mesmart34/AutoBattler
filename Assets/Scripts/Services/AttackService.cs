using System;
using Common;
using Common.Unit;
using Models;
using Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Services
{
    public class AttackService : ITickable
    {
        private const string AnimatorAttackTrigger = "Attack";
        private readonly float _attackTimeout;
        private float _attackTimeoutTimer;
        private int _attackStrength;
        
        public event Action<float, float> OnAttackTimeoutValueChange;

        /*[Inject]
        private readonly SignalBus _signalBus;

        [Inject]
        private AnimationService _animationService;*/

        /*[Inject]
        private HealthService _healthService;

        [Inject]
        private readonly BoardModel _board;

        [Inject]
        private readonly UnitFacade _unitFacade;

        [Inject]
        private UnitModel _unitModel;*/

        public UnitFacade _target { get; private set; }


        private bool _running;


        public AttackService()
        {
            /*_attackTimeout = _unitModel.UnitData.attackTimeout;
            _attackStrength = _unitModel.UnitData.attackStrength;*/
        }

        private void Attack()
        {
            //_animationService.PlayAttackAnimation(AnimatorAttackTrigger);
            _target.ApplyDamage(_attackStrength);
        }

        public void MakeReady()
        {
            _running = true;
        }
        
        public void FindTarget()
        {
            /*_target = _unitFacade.IsEnemy ? _board.FindPlayer(_unitFacade) : _board.FindEnemy();*/
        }


        public void Tick()
        {
            /*if (!_running)
                return;
            if (_target == null || !_target.IsAlive)
            {
                FindTarget();
                return;
            }

            _attackTimeoutTimer += Time.deltaTime;
            _attackTimeoutTimer = Math.Clamp(_attackTimeoutTimer, 0, _attackTimeout);
            if (_attackTimeoutTimer >= _attackTimeout)
            {
                _attackTimeoutTimer = 0;
                Attack();
            }

            OnAttackTimeoutValueChange?.Invoke(_attackTimeoutTimer, _attackTimeout);*/
        }

        public void TurnOn()
        {
            _running = true;
        }

        public void TurnOff()
        {
            _running = false;
        }
    }
}
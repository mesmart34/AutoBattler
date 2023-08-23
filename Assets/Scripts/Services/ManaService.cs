using System;
using Common.Unit;
using Scripts.Common.Unit;
using Scripts.Signals;
using Signals;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ManaService : ITickable, IInitializable, IDisposable
    {
        public event Action<int, int> OnManaValueChanged;  
        private int _maxMana;
        private int _mana;
        private float _timer;
        private int _regenAmount;
        private bool _running;
        
        [Inject]
        private readonly SignalBus _signalBus;


        public ManaService(UnitSettings unitSettings)
        {
            _mana = unitSettings.unitConfiguration.mana;
            _maxMana = unitSettings.unitConfiguration.mana;
            _regenAmount = unitSettings.unitConfiguration.manaRegenerationAmount;
        }
        
        
        public void SetMaxMana(int maxMana)
        {
            _maxMana = maxMana;
            _mana = maxMana;
        }

        public void TurnOn()
        {
            _running = true;
        }

        public void TurnOff()
        {
            _running = false;
        }

        public void Tick()
        {
            if (!_running)
                return;
            _timer += Time.deltaTime;
            if (_timer >= 1)
            {
                _mana += _regenAmount;
                _mana = Math.Clamp(_mana, 0, _maxMana);
                if (_mana == _maxMana)
                {
                    _mana = 0;
                }

                OnManaValueChanged?.Invoke(_mana, _maxMana);
                _timer = 0;
            }
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartBattleSignal>(TurnOn);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartBattleSignal>(TurnOn);
        }
    }
}
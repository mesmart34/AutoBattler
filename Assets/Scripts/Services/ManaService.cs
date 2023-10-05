using System;
using Common.Unit;
using Models;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ManaService : ITickable
    {
        private readonly UnitModel _unitModel;
        private int _maxMana;
        private int _mana;
        private float _time;
        private int _regenAmount;
        private bool _running;

        public Action<int, int> OnManaValueChanged;
        public Action OnManaRestored;

        public void SetMaxMana(int maxMana)
        {
            _maxMana = maxMana;
            _mana = maxMana;
        }

        public void Activate(UnitData unitData)
        {
            _mana = unitData.mana;
            _maxMana =  unitData.mana;
            _regenAmount =  unitData.manaRegenerationAmount;
            _running = true;
        }

        public void Deactivate()
        {
            _running = false;
        }
        
        public void Tick()
        {
            if (!_running)
            {
                return;
            }
            
            _time += Time.deltaTime;

            if (_time <= 1.0f)
            {
                return;
            }
            
            _mana += _regenAmount;
            _mana = Math.Clamp(_mana, 0, _maxMana);
            
            if (_mana == _maxMana)
            {
                OnManaRestored?.Invoke();
                _mana = 0;
            }

            OnManaValueChanged?.Invoke(_mana, _maxMana);
            
            _time = 0;
        }
    }
}
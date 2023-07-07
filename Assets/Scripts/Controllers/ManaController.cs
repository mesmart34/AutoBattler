using System;
using System.Collections;
using AutoBattler;
using UnityEngine;

namespace Controllers
{
    public class ManaController : MonoBehaviour
    {
        private Unit _unit;
        private int _mana;
        public int RegenerationAmountPerTick;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            StartCoroutine(Coroutine());
        }

        public int Mana
        {
            get => _mana;
            set
            {
                if (_mana <= 0)
                {
                    _mana = 0;
                    OnManaRestored?.Invoke();
                }
                _mana = value;
                OnManaChanged?.Invoke(_mana);
            }
        }
        
        public event Action<int> OnManaChanged;
        public event Action OnManaRestored;

        private IEnumerator Coroutine()
        {
            while (_unit._healthController.Health > 0)
            {
                _mana += RegenerationAmountPerTick;
                OnManaChanged?.Invoke(_mana); 
                yield return null; 
            }
        }
    }
}
using System;
using AutoBattler;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Unit))]
    public class HealthController : MonoBehaviour
    {
        private int _health;
        public int PhysicalResist;
        public int MagicalResist;
        
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    _health = 0;
                    OnHealthChanged?.Invoke(_health);
                    OnDied?.Invoke();
                    //return;
                }
                else
                {
                    OnHealthChanged?.Invoke(_health);
                }
            }
        }
        
        public event Action<int> OnHealthChanged;
        public event Action OnDied;
    }
}
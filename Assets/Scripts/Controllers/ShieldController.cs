using System;
using AutoBattler;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Unit))]
    public class ShieldController : MonoBehaviour
    {
        private int _shield;
        
        public int Shield
        {
            get => _shield;
            set
            {
                if (_shield <= 0)
                {
                    _shield = 0;
                    OnShieldBroke?.Invoke();
                }
                _shield = value;
                OnShieldChanged?.Invoke(_shield);
            }
        }
        
        public event Action<int> OnShieldChanged;
        public event Action OnShieldBroke;
    }
}
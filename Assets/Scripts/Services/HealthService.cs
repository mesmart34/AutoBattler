using System;
using Zenject;

namespace Services
{
    public class HealthService
    {
        private int _maxHealth;
        private int _health;
        
        public bool IsAlive => _health > 0;
        public event Action<int, int> OnHealthValueChanged;

        [Inject]
        private readonly SignalBus _signalBus;
        
        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
        }
        
        public void ApplyDamage(int damage)
        {
            _health -= damage;
            _health = Math.Clamp(_health, 0, _maxHealth);
            OnHealthValueChanged?.Invoke(_health, _maxHealth);
        }
    }
}
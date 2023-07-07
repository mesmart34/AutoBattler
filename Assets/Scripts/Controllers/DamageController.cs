using System;
using AutoBattler;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(HealthController))]
    public class DamageController : MonoBehaviour
    {
        private HealthController _healthController;
        private ShieldController _physicalShieldComponent;
        private ShieldController _magicalShieldComponent;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _physicalShieldComponent = GetComponent<ShieldController>();
            _magicalShieldComponent = GetComponent<ShieldController>();
        }

        public void ApplyDamage(Damage damage)
        {
            switch (damage.Type)
            {
                case DamageType.Physical:
                {
                    _physicalShieldComponent.Shield -= damage.Amount;
                    if (_physicalShieldComponent.Shield >= 0)
                        return;
                    var damageAfterPhysicalShield = -_physicalShieldComponent.Shield;
                    if (_healthController.PhysicalResist >= damageAfterPhysicalShield)
                        return;
                    _healthController.Health -= damageAfterPhysicalShield;
                    break;
                }
                case DamageType.Magical:
                    _magicalShieldComponent.Shield -= damage.Amount;
                    if (_magicalShieldComponent.Shield >= 0)
                        return;
                    var damageAfterMagicalShield = -_magicalShieldComponent.Shield;
                    if (_healthController.PhysicalResist >= damageAfterMagicalShield)
                        return;
                    _healthController.Health -= damageAfterMagicalShield;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
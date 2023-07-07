using System;
using System.Collections;
using System.Collections.Generic;
using AutoBattler;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controllers
{
    [RequireComponent(typeof(Unit))]
    public class AttackController : MonoBehaviour
    {
        public int Strength;
        public float Cooldown;
        [SerializeField]
        private float missProbability = 0.0f;
        public float MissProbability
        {
            get => missProbability;
            set => value = Math.Clamp(missProbability, 0, 1);
        }

        public DamageType DamageType;
        public Unit Target;

        private float _time = 0.0f;
        private HealthController _healthController;

        public Action OnMissed;
        public event Action<Unit> OnAttacked;
        public event Action<Damage, Unit> OnAttackPrepare;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
        }

        private IEnumerator Start()
        {
            while (_healthController.Health > 0)
            {
                if (_time >= Cooldown)
                {
                    Attack();
                    _time = 0.0f;
                }

                _time += Time.deltaTime;
                yield return null;
            }
        }

        private void Attack()
        {
            if (!Target || Target._healthController.Health <= 0)
            {
                Target = Board.Instance.GetTarget(GetComponent<Unit>());
            }

            if (!Target)
                return;


            var damage = new Damage()
            {
                Amount = Strength,
                Type = DamageType
            };
            OnAttackPrepare?.Invoke(damage, Target);
            if (Random.value <= MissProbability)
            {
                OnMissed?.Invoke();
                return;
            }
            Target.GetComponent<DamageController>()?.ApplyDamage(damage);
            OnAttacked?.Invoke(Target);
        }
    }
}
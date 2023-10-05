using System;
using System.Collections;
using Common.Effects;
using UnityEngine;

namespace Models
{
    public class EffectModel
    {
        public EffectSettings EffectSettings { get; private set; }
        private readonly Func<UnitModel, BoardModel, Action, Action, Action, IEnumerator> _doLogic;
        public delegate void SumDelegate(EffectModel original, EffectModel other);
        public SumDelegate Sum;
        public event Action<EffectModel> OnInitialize;
        public event Action<EffectModel> OnTick;
        public event Action<EffectModel> OnDispose;

        public EffectModel(EffectSettings effectSettings, System.Func<UnitModel, BoardModel, Action, Action, Action, IEnumerator> doLogic)
        {
            EffectSettings = effectSettings;
            _doLogic = doLogic;
        }
        
        private void OnInited()
        {
            OnInitialize?.Invoke(this);
        }

        private void OnTicked()
        {
            OnTick?.Invoke(this);
        }

        private void OnDisposed()
        {
            OnDispose?.Invoke(this);
        }

        public void Activate(MonoBehaviour runner, 
            UnitModel unitModel, 
            BoardModel boardModel)
        {
            runner.StartCoroutine(_doLogic(unitModel, boardModel, OnInited, OnTicked, OnDisposed));
        }

        public void Deactivate(MonoBehaviour runner, 
            UnitModel unitModel, 
            BoardModel boardModel)
        {
            if (runner != null)
            {
                runner.StopCoroutine(_doLogic(unitModel, boardModel, OnInited, OnTicked, OnDisposed));
                
            }
            OnDisposed();
        }
    }
}
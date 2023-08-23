using System;
using System.Collections.Generic;
using Models;
using Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Services
{
    public class EffectService : ITickable, IInitializable, IDisposable
    {
        private const string AnimatorHighlightTrigger = "Highlight";
        
        [SerializeField]
        private Transform effectBarTransform;

        [SerializeField]
        private GameObject effectPrefab;
        
        private Dictionary<IEffect, GameObject> _effectMap = new();
        private static readonly int Highlight = Animator.StringToHash(AnimatorHighlightTrigger);

        private UnitModel _model;
        private bool _ready;

        private readonly SignalBus _signalBus;
        
        

      
        
        private void OnEffectRemoved(IEffect effect)
        {
            //Destroy(_effectMap[effect]);
            effect.OnEffectCooldown -= HighlightEffect;
            effect.OnEffectRemove -= OnEffectRemoved;
        }

        public void AddEffect(IEffect effect)
        {
            
            /*var effectInstance = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity, effectBarTransform);
            var icon = Resources.Load<Sprite>(effect.IconPath);
            effectInstance.GetComponent<Image>().sprite = icon;
            _effectMap[effect] = effectInstance;*/
            
            effect.OnEffectCooldown += HighlightEffect;
            effect.OnEffectRemove += OnEffectRemoved;
        }

        private void HighlightEffect(IEffect effect)
        {
            var instance = _effectMap[effect];
            instance.GetComponentInChildren<Animator>().SetTrigger(AnimatorHighlightTrigger);
        }

        public void RemoveEffect(IEffect effect)
        {
            _effectMap.Remove(effect);
        }

        public void RemoveAllEffects()
        {
            _effectMap.Clear();
        }

        public void Tick()
        {
            foreach (var effect in _effectMap.Keys)
            {
               effect.Tick(_model);
            }
        }

        public void Initialize()
        {
            
            foreach (var effect in _effectMap.Keys)
            {
                effect.Initialize(_model);
            }
        }

        public void Dispose()
        {
            foreach (var effect in _effectMap.Keys)
            {
                effect.Dispose(_model);
            }
        }
    }
}
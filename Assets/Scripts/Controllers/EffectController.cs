using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Effects;
using Factories;
using Models;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class EffectController : MonoBehaviour
    {
        private bool _activated;
        private readonly Dictionary<string, int> _effectStackCounter = new();
        private readonly Dictionary<EffectModel, EffectView> _effectView = new();
        private readonly Dictionary<string, EffectModel> _singleEffects = new();
        private readonly List<EffectModel> _effects = new();

        [Inject]
        private EffectFactory _effectFactory;

        [Inject]
        private UnitModel _unitModel;

        [Inject]
        private BoardModel _boardModel;

        private void Start()
        {
            _effectFactory.Load();
        }

        public bool TryFindAndAddEffect(string effectName)
        {
            var effectSo = Resources.Load<EffectScriptable>($"Effects/{effectName}");
            if (effectSo == null)
            {
                Debug.LogError($"Effect not found: {effectName}");
                return false;
            }
            var effectSettings = new EffectSettings()
            {
                Sprite = effectSo.effectSettings.Sprite,
                Stackable = effectSo.effectSettings.Stackable,
                MaxStacked = effectSo.effectSettings.MaxStacked,
                Name = effectSo.effectSettings.Name,
                Summable =  effectSo.effectSettings.Summable,
                TimeExist = effectSo.effectSettings.TimeExist
            };
            var hasEffect = _effectStackCounter.ContainsKey(effectSettings.Name);
            _effectStackCounter.TryGetValue(effectSettings.Name, out var value);
            var effect = new EffectModel(effectSettings, effectSo.DoLogic);
            effect.Sum += effectSo.Sum;
            if (effectSettings.Stackable)
            {
                if (value >= effectSettings.MaxStacked && effectSettings.MaxStacked != 0) 
                    return true;
                AddEffectAndCreateView(effect);
            }
            else
            {
                if (!hasEffect)
                {
                    AddEffectAndCreateView(effect);
                    if (effectSettings.Summable)
                    {
                        _singleEffects[effect.EffectSettings.Name] = effect;
                    }
                }
                else
                {
                    if (_singleEffects.TryGetValue(effect.EffectSettings.Name, out var singleEffect))
                    {
                        singleEffect.Sum(_singleEffects[effect.EffectSettings.Name], effect);
                    }
                }
            }
            
            return true;
        }

        public void Activate()
        {
            _activated = true;
            TryFindAndAddEffect("SilentEffect");
            TryFindAndAddEffect("SilentEffect");
            TryFindAndAddEffect("FireEffect");
            TryFindAndAddEffect("FireEffect");
            foreach (var effect in _effects)
            {
                effect.Activate(this, _unitModel, _boardModel);
            }
        }
        
        public void Deactivate()
        {
            _activated = false;
            foreach (var effect in _effects)
            {
                if (effect != null && _effectView.TryGetValue(effect, out var value))
                {
                    effect.Deactivate(value, _unitModel, _boardModel);
                }
            }
        }

        private void OnEffectDisposed(EffectModel effectModel)
        {
            if (!_effectView.ContainsKey(effectModel))
                return;
            var view = _effectView[effectModel];
            _effectView.Remove(effectModel);
            Destroy(view.gameObject);
            Debug.Log("DELETED");
            /*effectModel.OnTick -= _effectView[effectModel].Highlight;
            effectModel.OnDispose -= OnEffectDisposed;
            //effectModel.Deactivate(_effectView[effectModel]);
            Destroy(_effectView[effectModel].gameObject);
            _effectView.Remove(effectModel);
            _effects.Remove(effectModel);*/
        }

        private void AddEffectAndCreateView(EffectModel effectModel)
        {
            var effect = _effectFactory.Create(transform);
            var effectView = effect.GetComponent<EffectView>();
            effectView.GetComponent<Image>().sprite = effectModel.EffectSettings.Sprite;
            effectModel.OnTick += effectView.Highlight;
            effectModel.OnDispose += OnEffectDisposed;
            _effects.Add(effectModel);
            _effectStackCounter.TryAdd(effectModel.EffectSettings.Name, 0);
            _effectView.TryAdd(effectModel, effectView);
            _effectStackCounter[effectModel.EffectSettings.Name]++;
            _effectView[effectModel] = effectView;
            if (_activated)
            {
                effectModel.Activate(effectView, _unitModel, _boardModel);
            }
        }
    }
}
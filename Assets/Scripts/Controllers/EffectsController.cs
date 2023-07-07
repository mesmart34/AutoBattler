using System.Collections.Generic;
using System.Linq;
using AutoBattler;
using Effects;
using UnityEngine;

namespace Controllers
{
    public class EffectsController : MonoBehaviour
    {
        public List<EffectData> _effects = new() ;
        private Unit _unit;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            //AddEffect(new BurningEffect());
        }
        
        public void AddEffect(EffectData effectData)
        {
            var n = _effects.Count(x => x.Name == effectData.Name);
            
            if (effectData.Stackable && n >= effectData.MaxStacked)
                return;
            
            if(!effectData.Stackable && n > 0)
                return;
            
            _effects.Add(effectData);
            effectData.OnInitialize(_unit);
        }
    }
}
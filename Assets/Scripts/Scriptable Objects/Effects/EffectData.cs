using System.Collections;
using UnityEngine;

namespace AutoBattler
{
    public abstract class EffectData : ScriptableObject
    {
        public string Name;
        public string Description;
        public bool Stackable;
        public int MaxStacked;

        public abstract void OnInitialize(Unit unit);
    }
}
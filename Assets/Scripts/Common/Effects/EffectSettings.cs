using System;
using UnityEngine;

namespace Common.Effects
{
    [Serializable]
    public class EffectSettings
    {
        public string Name;
        public Sprite Sprite;
        public bool Stackable;
        public int MaxStacked;
        public bool Summable;
        public float TimeExist;
    }
}
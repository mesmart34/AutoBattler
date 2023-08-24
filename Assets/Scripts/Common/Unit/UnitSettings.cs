using System;
using Scripts.Common.Unit;
using UnityEngine;

namespace Common.Unit
{
    [Serializable]
    public class UnitSettings
    {
        public UnitConfiguration unitConfiguration;
        public SpriteRenderer spriteRenderer;
        public Canvas canvas;
        public GameObject bars;
    }
}
using System;
using UnityEngine;

namespace Common.Unit
{
    [Serializable]
    public class UnitSettings
    {
        public SpriteRenderer spriteRenderer;
        public Canvas canvas;
        public GameObject bars;
        public LineRenderer lineRenderer;
        public Transform linePoint;
        public GameObject questionMark;
    }
}
using UnityEngine;
using Zenject;

namespace Common.Unit
{
    public class UnitHighlighter : IInitializable
    {
        private readonly string _outlineParameterName = "_outline";
        private Material _material;
        
        [Inject]
        private UnitSettings _unitSettings;

        public void ShowHighlight()
        {
            _material.SetInt(_outlineParameterName, 1);
        }

        public void HideHighlight()
        {
            _material.SetInt(_outlineParameterName, 0);
        }

        public void Initialize()
        {
            _material = _unitSettings.spriteRenderer.material;
        }
    }
}
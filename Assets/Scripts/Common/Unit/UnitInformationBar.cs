using UnityEngine;

namespace Common.Unit
{
    public class UnitInformationBar : MonoBehaviour
    {
        private float _maxWidth;
        
        [SerializeField]
        private RectTransform bar;

        private void Awake()
        {
            _maxWidth = bar.sizeDelta.x;
        }

        public void ResizeBar(float value)
        {
            var height = bar.sizeDelta.y;
            var width = _maxWidth * value;
            bar.sizeDelta = new Vector2(width, height);
        }
        
    }
}
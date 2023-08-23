using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class DamagePopup : MonoBehaviour
    {
        public float ExistTime { get; set; }
        public int Value { get; set; }
        public Vector2 CanvasSize { get; set; }
        public Color color { get; set; }

        private RectTransform _rectTransform;
        private TextMeshProUGUI _text;


        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _rectTransform = GetComponent<RectTransform>();
            var textRect = _rectTransform.GetComponent<RectTransform>();
            var size = CanvasSize;
            // var sequence = DOTween.Sequence();
            var newPos = new Vector2(size.x * 0.25f + Random.value * size.x * 0.5f, size.y * 0.25f);
            textRect.anchoredPosition = newPos;
            _text.text = Value.ToString();
            //textRect.DOLocalJump(new Vector3(newPos.x + 0.5f * size.x, size.y * -0.5f, 0), 1, 1, speed);
            textRect.DOMoveY(size.y * -0.5f, ExistTime);
            _text.DOFade(0.0f, ExistTime * 0.5f);
            Destroy(gameObject, ExistTime);
        }
    }
}
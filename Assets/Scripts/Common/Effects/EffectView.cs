using System;
using DG.Tweening;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Effects
{
    public class EffectView : MonoBehaviour
    {
        private const float Duration = 0.5f;

        [SerializeField]
        private Image spriteImage;

        public void Highlight(EffectModel effectModel)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(spriteImage.DOFade(1.0f, Duration));
            sequence.Append(spriteImage.DOFade(0.0f, Duration));
        }
    }
}
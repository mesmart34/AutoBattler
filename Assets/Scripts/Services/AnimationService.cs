using System;
using System.Collections;
using System.Linq;
using Common;
using DG.Tweening;
using Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Services
{
    public class AnimationService : IInitializable, IDisposable
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Sprite[] _mainSprite;
        private readonly Texture2D[] _emissionMaps;
        private readonly Material _material;
        private readonly float _secondsBetweenFrames = 0.5f;
        private Sequence _sequence;
        private bool _running;

        [Inject]
        private readonly UnitFacade _unitFacade;


        public AnimationService(SpriteRenderer spriteRenderer, Sprite[] mainSprites, Texture2D[] emissionMaps)
        {
            _spriteRenderer = spriteRenderer;
            _material = _spriteRenderer.material;
            _mainSprite = mainSprites;
            _emissionMaps = emissionMaps;
        }

        public void PlayAttackAnimation(string triggerName)
        {
            _sequence.Append(_spriteRenderer.transform.DOPunchPosition(Vector3.right, 0.5f, 0, 0.0f));
        }

        public void PlayRecieveDamageAnimation()
        {
            _sequence.Append(_spriteRenderer.transform.DOLocalJump(Vector3.zero, 2, 1, 0.5f));
        }

        public void Initialize()
        {
            _unitFacade.OnInitilize += OnGameStarted;
            _sequence = DOTween.Sequence();
        }

        private IEnumerator AnimatorCoroutine()
        {
            _running = true;
            var textureCounter = 0;
            while (_running)
            {
                textureCounter++;
                if (textureCounter >= _mainSprite.Length)
                {
                    textureCounter = 0;
                }
                _spriteRenderer.sprite = _mainSprite[textureCounter];
                if (_emissionMaps.Any())
                {
                    _material.SetTexture("_Emission_Texture", _emissionMaps[textureCounter]);
                }
                yield return new WaitForSeconds(_secondsBetweenFrames);
            }
        }

        public void TurnOff()
        {
            _running = false;
            _unitFacade.StopCoroutine(AnimatorCoroutine());
        }
        
        private void OnGameStarted()
        {
            _unitFacade.StartCoroutine(AnimatorCoroutine());
        }

        public void Dispose()
        {
            _unitFacade.OnInitilize -= OnGameStarted;
            _unitFacade.StopCoroutine(AnimatorCoroutine());
            _sequence.Kill();
        }
    }
}
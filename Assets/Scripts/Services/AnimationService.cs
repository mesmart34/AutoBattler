using System;
using System.Collections;
using System.Linq;
using Common.Unit;
using DG.Tweening;
using Models;
using UnityEngine;
using Zenject;

namespace Services
{
    public class AnimationService : IInitializable, ITickable, IDisposable
    {
        private readonly UnitModel _unitModel;
        private SpriteRenderer _spriteRenderer;
        private Sprite[] _mainSprite;
        private Texture2D[] _emissionMaps;
        private Material _material;
        private readonly float _secondsBetweenFrames = 0.5f;
        private Sequence _sequence;
        private bool _running;
        private float _time;
        private int _textureCounter;
        
        [Inject]
        private UnitSettings _unitSettings;

        public void PlayAttackAnimation()
        {
            if (!_running)
            {
                return;
            }

            var attackDirection = _unitSettings.spriteRenderer.flipX ? Vector3.left : Vector3.right;
            _sequence.Append(_spriteRenderer.transform.DOPunchPosition(attackDirection, 0.5f, 0, 0.0f));
        }

        public void PlayLockedAnimation()
        {
            _running = false;
            var darkColor = 0.0f;
            _spriteRenderer.color = new Color(darkColor, darkColor, darkColor, 1.0f);
            _unitSettings.questionMark.SetActive(true);
        }
        
        public void PlayRecieveDamageAnimation()
        {
            if (!_running)
            {
                return;
            }
            _sequence.Append(_spriteRenderer.transform.DOLocalJump(Vector3.zero, 2, 1, 0.5f));
        }

        public void PlayDeadAnimation()
        {
            var darkColor = 0.5f;
            _spriteRenderer.color = new Color(darkColor, darkColor, darkColor, 0.5f);
        }

        public void Initialize()
        {
            _sequence = DOTween.Sequence();
        }
        
        public void Activate(UnitData unitData)
        {
            _spriteRenderer = _unitSettings.spriteRenderer;
            _material = _spriteRenderer.material;
            _mainSprite = unitData.sprite;
            _emissionMaps = unitData.emissionMap;
            _running = true;
        }
        
        public void Deactivate()
        {
            _running = false;
            _spriteRenderer.sprite = _mainSprite.First();
        }

        public void Dispose()
        {
            _sequence.Kill();
        }

        public void Tick()
        {
            if (!_running)
            {
                return;
            }

            _time += Time.deltaTime;

            if (!(_time >= _secondsBetweenFrames))
            {
                return;
            }
            
            _textureCounter++;
            if (_textureCounter >= _mainSprite.Length)
            {
                _textureCounter = 0;
            }
            _spriteRenderer.sprite = _mainSprite[_textureCounter];
            if (_emissionMaps.Any())
            {
                _material.SetTexture("_Emission_Texture", _emissionMaps[_textureCounter]);
            }
                
            _time = 0.0f;
        }
    }
}
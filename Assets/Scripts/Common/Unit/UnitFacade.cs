using System;
using Common.Board;
using Models;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Common.Unit
{
    public class UnitFacade : MonoBehaviour, IInitializable
    {
        private const string MaterialOutline = "_outline";
        private Material _material;
        private bool _dragged;
        private bool _lineVisible;
        
        public PlatformFacade Platform { get; set; }

        [Inject]
        private UnitModel _unitModel;

        [Inject]
        private UnitSettings _unitSettings;

        [Inject]
        private UnitHighlighter _unitHighlighter;

        [Inject]
        private HealthService _healthService;

        public UnitData UnitData => _unitModel.UnitData;

        public bool IsAlive => _unitModel.IsAlive;
        
        public Action OnUnitDie;
        
        private void Awake()
        {
            _lineVisible = true;
            _material = _unitSettings.spriteRenderer.material;
            _healthService.OnHealthValueChanged += (int current, int max) =>
            {
                if (current <= 0)
                {
                    OnUnitDie?.Invoke();
                }
            };
        }

        public void SetUnitData(UnitData unitData)
        {
            _unitModel.SetupWithUnitData(unitData);
            _unitSettings.spriteRenderer.flipX = unitData.invertSpriteHorizontally;
            _unitSettings.spriteRenderer.sprite = unitData.sprite[0];
        }

        public void ApplyDamage(int damage) 
        {
            _unitModel.ApplyDamage(damage);
        }
        
        public virtual void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            _unitHighlighter.ShowHighlight ();
        }

        private void OnMouseExit()
        {
            _unitHighlighter.HideHighlight();
        }

        private void Update()
        {
            /*if (!_dragged && _unitModel.UnitState == UnitState.Idle)
            {
                DrawLineToTarget();
            }*/
        }

        private void OnMouseDown()
        {
            //_dragged = true;
            // _unitMoveController.SetTarget(transform);
            //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 1, Input.mousePosition.y));
        }

        private void OnMouseUp()
        {
           // _dragged = false;
        }

        public void Die()
        {
            /*var color = _spriteRenderer.color;
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
            _barController.Hide();*/
        }

        public void FindTarget()
        {
            _unitModel.FindTarget();
        }

        public void BeginBattle()
        {
            _unitModel.SetBarsActive(true);
            _unitModel.Activate();
        }

        public void EndBattle()
        {
            _unitModel.SetBarsActive(false);
            _unitModel.Deactivate();
        }

        private void DrawLineToTarget()
        {
            /*if (IsEnemy)
            {
                _lineRenderer.startColor = new Color(200, 100, 100);
            }

            _lineRenderer.enabled = _lineVisible;
            _lineRenderer.useWorldSpace = true;
            if (Target != null)
            {
                _lineRenderer.SetPositions(new[]
                {
                    _linePoint.position,
                    Target._linePoint.transform.position
                });
            }*/
        }

        public virtual void Initialize()
        {
            
        }
    }
}
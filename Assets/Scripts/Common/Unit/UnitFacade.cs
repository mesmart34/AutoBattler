using System;
using Common.Board;
using Models;
using UnityEngine;
using Zenject;

namespace Common.Unit
{
    public class UnitFacade : MonoBehaviour, IInitializable
    {
        private const string MaterialOutline = "_outline";
        private Material _material;
        private bool _dragged;
        private bool _lineVisible;
        //private SpriteRenderer _spriteRenderer;
        public PlatformFacade Platform { get; set; }
        public event Action OnInitilize;

        /*[Inject]
        private SignalBus _signalBus;

        [Inject]
        private HealthService _healthService;

        [Inject]
        private AttackService _attackService;

        [Inject]
        private UnitModel _unitModel;

        [Inject]
        private UnitMover _unitMover;

        [Inject]
        private BarController _barController;*/

        /*
        public UnitFacade Target
        {
            get => _attackService._target;
        }

        public bool FindNearestTarget
        {
            get => _unitModel.FindNearestTarget;
        }
        */
        
        [Inject]
        private UnitModel _unitModel;

        [Inject]
        private UnitSettings _unitSettings;

        [Inject]
        public void Construct()
        {
            
        }

        public UnitData UnitData => _unitModel.UnitData;

        private void Awake()
        {
            _lineVisible = true;
            /*_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _material = _spriteRenderer.material;*/
            OnInitilize?.Invoke();
            /*_signalBus.Subscribe<StartBattleSignal>(OnBattleStart);*/
        }

        public void SetUnitData(UnitData unitData)
        {
            _unitModel.SetupWithUnitData(unitData);
            _unitSettings.spriteRenderer.sprite = unitData.sprite[0];
        }

        public void PrepareMode()
        {
           // _unitModel.PrepareMode();
        }

        public void ApplyDamage(int damage)
        {
            //_unitModel.ApplyDamage(damage);
        }

        private void OnMouseEnter()
        {
            //_material.SetInt("_outline", 1);
        }

        private void OnMouseExit()
        {
           // _material.SetInt("_outline", 0);
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
            /*_attackService.FindTarget();*/
        }

        public void StartBattle()
        {
            /*_unitModel.StartBattle();
            _lineVisible = false;*/
        }

        public void BattleEnd()
        {
            //_lineVisible = true;
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
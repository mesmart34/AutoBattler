using System;
using System.Collections.Generic;
using Code.Unit;
using Common;
using Common.Board;
using JetBrains.Annotations;
using Scripts.Signals;
using Signals;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public class UnitMover : MonoBehaviour
    {
        private PlatformFacade _currentPlatform;
        private UnitFacade _selectedUnit;
        private Camera _camera;
        private bool _running;
        private Vector3 _unitPosition;
        public bool _tavern;
        private PlatformFacade _selectedUnitPlatform;
        private Dictionary<UnitFacade, Vector3> _unitInitialPositions = new();
        
        [SerializeField]
        private LayerMask boardLayer;
        
        [SerializeField]
        private LayerMask unitLayer;
        
        [SerializeField]
        private float cursorFollowSpeed = 3.0f;

        [SerializeField]
        private float verticalUnitOffsetWhenDragged = 2.0f;

        [Inject]
        private readonly SignalBus _signalBus;

        private void Awake()
        {
            _camera = Camera.main;
            _signalBus.Subscribe<StartBattleSignal>(OnBattleStart);
        }

        private void OnBattleStart()
        {
            _running = true;
        }

        private void TavernMove()
        {
             if (Input.GetButtonDown("Fire1"))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, unitLayer))
                {
                    if (hit.collider.CompareTag("Unit"))
                    {
                        var unitFacade = hit.collider.gameObject.GetComponent<UnitFacade>();
                        if (unitFacade.Platform != null)
                        {
                            _selectedUnitPlatform = unitFacade.Platform;
                        }
                        _unitPosition = unitFacade.transform.position;
                        if (!_unitInitialPositions.ContainsKey(unitFacade))
                        {
                            _unitInitialPositions[unitFacade] = _unitPosition;
                        }
                        if (unitFacade.IsEnemy)
                            return;
                        _selectedUnit = unitFacade;
                    }
                }
            }
            if (Input.GetButtonUp("Fire1") && _selectedUnit != null)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, boardLayer))
                {
                    if (hit.collider.CompareTag("Platform"))
                    {
                        var platform = hit.collider.gameObject.GetComponent<PlatformFacade>();
                        if (platform._draggable)
                        {
                            if (platform.Busy)
                            {
                                platform.unitFacade.transform.position = _unitInitialPositions[platform.unitFacade];
                            }
                            platform.Clear();
                            platform.SetUnit(_selectedUnit);
                            _selectedUnit.transform.position = platform.transform.position;
                            _selectedUnit = null;
                        }
                    }
                    else
                    {
                        _selectedUnit.transform.position =  _unitInitialPositions[_selectedUnit];
                        _selectedUnitPlatform.unitFacade = null;
                        _selectedUnitPlatform = null;
                        _selectedUnit = null;
                    }
                }
            }

            if (_selectedUnit)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, boardLayer))
                {
                    _selectedUnit.transform.position = Vector3.Lerp(_selectedUnit.transform.position,
                        hit.point + Vector3.up * verticalUnitOffsetWhenDragged,
                        Time.deltaTime * cursorFollowSpeed);
                }
            }
        }

        private void PrepareMove()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, unitLayer))
                {
                    if (hit.collider.CompareTag("Unit"))
                    {
                        var unitFacade = hit.collider.gameObject.GetComponent<UnitFacade>();
                        _unitPosition = unitFacade.transform.position;
                        if (unitFacade.IsEnemy)
                            return;
                        _selectedUnit = unitFacade;
                        _currentPlatform = unitFacade.Platform;
                    }
                }
            }
            if (Input.GetButtonUp("Fire1") && _selectedUnit != null)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, boardLayer))
                {
                    if (hit.collider.CompareTag("Platform"))
                    {
                        var platform = hit.collider.gameObject.GetComponent<PlatformFacade>();
                        if (platform._draggable && !platform.Busy)
                        {
                            _currentPlatform.Clear();
                            _currentPlatform = platform;
                            _currentPlatform.SetUnit(_selectedUnit);
                            _signalBus.Fire<UnitPositionChangeSignal>();
                        }
                    }
                    _selectedUnit.transform.position = _currentPlatform.transform.position;
                    _selectedUnit = null;
                }
            }

            if (_selectedUnit)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, boardLayer))
                {
                    _selectedUnit.transform.position = Vector3.Lerp(_selectedUnit.transform.position,
                        hit.point + Vector3.up * verticalUnitOffsetWhenDragged,
                        Time.deltaTime * cursorFollowSpeed);
                }
            }
        }
        
        private void Update()
        {
            if (_running)
            {
                return;
            }

            if (_tavern)
            {
                TavernMove();
            }
            else
            {
                PrepareMove();
            }
           
        }
    }
}
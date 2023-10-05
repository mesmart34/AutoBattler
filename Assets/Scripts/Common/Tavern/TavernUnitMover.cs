using System.Linq;
using Common.Board;
using Common.Unit;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Common.Tavern
{
    public class TavernUnitMover : IInitializable, ITickable
    {
        private const string PlatformTag = "Platform";
        private Camera _camera;
        private LayerMask _unitLayer;
        private LayerMask _boardLayer;
        private float _verticalUnitOffset;
        private float _moveSpeed;

        private UnitFacade _selectedUnit;
        
        [Inject]
        private TavernSettings _tavernSettings;

        [Inject]
        private TavernModel _tavernModel;

        public void Tick()
        {
            var overUI = EventSystem.current.IsPointerOverGameObject();
            if (Input.GetButtonDown("Fire1") && !overUI)
            {
                MouseDown();
            } else if (Input.GetButtonUp("Fire1"))
            {
                MouseUp();
            }
            else if(Input.GetButton("Fire1"))
            {
                MouseDrag();
            }
        }

        private void MouseUp()
        {
            if (_selectedUnit == null)
            {
                return;
            }
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, 100000, _boardLayer))
            {
                return;
            }
            
            if (hit.collider.CompareTag(PlatformTag))
            {
                var platformFacade = hit.collider.gameObject.GetComponent<PlatformFacade>();
                if (platformFacade.Busy)
                {
                    ReturnUnitToSpawnPoint();
                    return;
                }
                platformFacade.SetUnit(_selectedUnit);
                _selectedUnit = null;
                SaveCurrentBoardState();
            }
            else
            {
                ReturnUnitToSpawnPoint();
            }
        }
        
        private void MouseDown()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100000, _unitLayer))
            {
                _selectedUnit = hit.collider.gameObject.GetComponent<UnitFacade>();
                if (_selectedUnit.Platform != null)
                {
                    _selectedUnit.Platform.Clear();
                }
                SaveCurrentBoardState();
            }
        }

        private void MouseDrag()
        {
            if (Input.GetButton("Fire1") && _selectedUnit != null)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100000, _boardLayer))
                {
                    _selectedUnit.transform.position = Vector3.Lerp(_selectedUnit.transform.position,
                        hit.point + Vector3.up * _verticalUnitOffset,
                        Time.deltaTime * _moveSpeed);
                }else
                {
                    ReturnUnitToSpawnPoint();
                }
            }
        }

        private void SaveCurrentBoardState()
        {
            _tavernSettings.playerBoardConfiguration.units.Clear();
            var heroes = _tavernModel.Heroes.Where(x => x.Platform != null);
            foreach (var hero in heroes)
            {
                var platformLogicPosition = hero.Platform.position;
                _tavernSettings.playerBoardConfiguration.units.Add(new UnitSetup()
                {
                    name = hero.UnitData.name,
                    position = platformLogicPosition

                });
            }
        }

        private void ReturnUnitToSpawnPoint()
        {
            var spawnPoint = _tavernSettings.heroesToSpawn.FirstOrDefault(x =>
                x.hero.unitData.name == _selectedUnit.UnitData.name)?.spawnPoint;

            if (spawnPoint != null)
            {
                _selectedUnit.transform.position = spawnPoint.position;
                if (_selectedUnit.Platform != null)
                {
                    _selectedUnit.Platform.unitFacade = null;
                    _selectedUnit.Platform = null;
                }
                _selectedUnit = null;
                SaveCurrentBoardState();
            }
        }
        
        public void Initialize()
        {
            _camera = _tavernSettings.camera;
            _unitLayer = _tavernSettings.unitLayer;
            _verticalUnitOffset = _tavernSettings.verticalUnitOffset;
            _moveSpeed = _tavernSettings.moveSpeed;
            _boardLayer = _tavernSettings.boardLayer;
        }
    }
}
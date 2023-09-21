using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Enemy;
using Common.Map;
using Controllers;
using DG.Tweening;
using Factories;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Zenject.ReflectionBaking.Mono.Cecil;

namespace Models
{
    public class MapModel : IInitializable
    {
        private readonly Dictionary<MapLevelWrapper, GameObject> _levelObjectType = new();
        private LevelConfiguration[] _levelConfigurations;
        private const int MapWidth = 10;
        private readonly MapState _mapState;
        private PlayerConfiguration _playerConfiguration;

        [Inject]
        private LoadingScreenController _loadingScreenController;
        
        [Inject]
        private MapIconFactory _mapIconFactory;

        [Inject]
        private readonly MapSettings _mapSettings;
        
        [Inject]
        private AsyncHandler _asyncHandler;

        public bool IsMapOpened { get; private set; }

        public MapModel(MapSettings mapSettings)
        {
            _mapSettings = mapSettings;
            _mapState = _mapSettings.mapState;
            _playerConfiguration = _mapSettings.playerConfiguration;
        }

        public bool IsFinished()
        {
            return MapWidth == _playerConfiguration.mapProgress + 1;
        }
        
        public void MoveNext()
        {
            _playerConfiguration.mapProgress += 1;
            UpdateMapRelativeToProgress();
        }
        
        private void LoadLevels()
        {
            _mapIconFactory.Load();
            _levelConfigurations = Resources.LoadAll<LevelConfiguration>("Levels");
        }

        public void Unlock()
        {
            _mapSettings.openCloseButton.GetComponent<Button>().interactable = true;
        }
        
        public void Lock()
        {
            _mapSettings.openCloseButton.GetComponent<Button>().interactable = false;
        }

        private void UpdateMapRelativeToProgress()
        {
            var levels = _mapState.level;
            for (var colId = 0; colId < levels.Count; colId++)
            {
                var levelIcons = levels[colId];
                if (colId < _playerConfiguration.mapProgress)
                {
                    foreach (var levelIcon in levelIcons.levelColumn)
                    {
                        if (levelIcon.levelType == LevelType.None)
                        {
                            continue;
                        }
                        _levelObjectType[levelIcon].GetComponent<Button>().interactable = false;
                        _levelObjectType[levelIcon].GetComponent<Image>().color = _mapSettings.iconColorLevelPassed;
                    }
                }
                else if (colId > _playerConfiguration.mapProgress)
                {
                    foreach (var levelIcon in levelIcons.levelColumn)
                    {
                        if (levelIcon.levelType == LevelType.None)
                        {
                            continue;
                        }

                        _levelObjectType[levelIcon].GetComponent<Button>().interactable = false;
                        _levelObjectType[levelIcon].GetComponent<Image>().color = _mapSettings.iconColorLevelNext;
                    }
                }
                else
                {
                    foreach (var levelIcon in levelIcons.levelColumn)
                    {
                        if (levelIcon.levelType == LevelType.None)
                        {
                            continue;
                        }
                        
                        _levelObjectType[levelIcon].GetComponent<Button>().interactable = true;
                        _levelObjectType[levelIcon].GetComponent<Image>().color = _mapSettings.iconColorLevelCurrent;
                    }
                }
            }
        }

        private void OnIconClick(int rowIndex)
        {
            _playerConfiguration.mapLevelInColumn = rowIndex;
            _asyncHandler.StartCoroutine(LoadNextLevel());
        }
        
        private IEnumerator LoadNextLevel()
        {
            _loadingScreenController.Open();
            var loadSceneAsync = SceneManager.LoadSceneAsync("Battle");
            var timer = 0.0f;
            loadSceneAsync.allowSceneActivation = false;
            while (!loadSceneAsync.isDone && timer < 2.0f)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            loadSceneAsync.allowSceneActivation = true;
        }
        
        private void GenerateColumn(MapColumnWrapper column)
        {
            for (var index = 0; index < column.levelColumn.Count; index++)
            {
                var level = column.levelColumn[index];
                var levelType = level.levelType;
                var button = _mapIconFactory.Create(_mapSettings.rows[index]);
                if (levelType != LevelType.None)
                {
                    var index1 = index;
                    button.GetComponent<Button>().onClick.AddListener(() => { OnIconClick(index1); });
                }
                var image = button.GetComponent<Image>();
                _levelObjectType[level] = button;
                switch (levelType)
                {
                    case LevelType.Enemy:
                        image.sprite = _mapSettings.enemyIcon;
                        break;
                    case LevelType.None:
                        image.sprite = null;
                        image.color = new Color(0, 0, 0, 0);
                        break;
                    case LevelType.Boss:
                        image.sprite = _mapSettings.bossIcon;
                        break;
                }
            }
        }

        public void GenerateLevelPipeline()
        {
            _playerConfiguration.mapProgress = 0;
            _playerConfiguration.mapLevelInColumn = 0;
            var mapState = _mapState.level;
            var mapHeight = 3;
            for (var i = 0; i < MapWidth; i++)
            {
                var levelColumn = new List<MapLevelWrapper>()
                {
                    new MapLevelWrapper()
                    {
                        levelType = LevelType.None,
                    },
                    new MapLevelWrapper()
                    {
                        levelType = LevelType.None,
                    },
                    new MapLevelWrapper()
                    {
                        levelType = LevelType.None,
                    }
                };
                if (i == 0)
                {
                    for (var y = 0; y < mapHeight; y++)
                    {
                        levelColumn[y].levelType = LevelType.Enemy;
                        levelColumn[y].levelConfiguration =
                            _levelConfigurations[Random.Range(0, _levelConfigurations.Length)];
                    }
                }
                else if (i > 0 && i < MapWidth - 1)
                {
                    var enemiesInRow = Random.Range(1, 3);
                    for (var y = 0; y < enemiesInRow; y++)
                    {
                        var randomPosition = Random.Range(0, mapHeight);
                        levelColumn[randomPosition].levelType = LevelType.Enemy;
                        levelColumn[randomPosition].levelConfiguration =
                            _levelConfigurations[Random.Range(0, _levelConfigurations.Length)];
                    }
                }
                else if (i == MapWidth - 1)
                {
                    var randomPosition = Random.Range(0, mapHeight);
                    levelColumn[randomPosition].levelType = LevelType.Boss;
                    levelColumn[randomPosition].levelConfiguration =
                        _levelConfigurations[Random.Range(0, _levelConfigurations.Length)];
                }

                mapState.Add(new MapColumnWrapper()
                {
                    levelColumn = levelColumn
                });
            }
        }

        public void GenerateViewForMap()
        {
            foreach (var col in _mapState.level)
            {
                GenerateColumn(col);
            }
        }

        public void Close()
        {
            IsMapOpened = false;
            var mapViewHeight = _mapSettings.mapRectTransform.rect.height;
            _mapSettings.mapRectTransform.DOJumpAnchorPos(Vector2.zero, 2.0f, 3, 0.25f).onComplete += () =>
            {
                /*_mapSettings.closeButtonText.text = "Open";*/
            };
        }

        public void Open()
        {
            IsMapOpened = true;
            var mapViewHeight = _mapSettings.mapRectTransform.rect.height;
            _mapSettings.mapRectTransform.DOJumpAnchorPos(new Vector2(0.0f, -mapViewHeight), 2.0f, 3, 0.25f)
                .onComplete += () => { /*_mapSettings.closeButtonText.text = "Close";*/ };
        }

        public MapLevelWrapper GetCurrentLevel()
        {
            var row = _mapState.level[_playerConfiguration.mapProgress];
            var level = row.levelColumn[_playerConfiguration.mapLevelInColumn];
            return level;
        }


        public void Initialize()
        {
            LoadLevels();
            if (!_mapState.level.Any())
            {
                GenerateLevelPipeline();
            }
            GenerateViewForMap();
            UpdateMapRelativeToProgress();
        }
    }
}
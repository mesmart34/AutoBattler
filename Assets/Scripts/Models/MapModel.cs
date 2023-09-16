using System.Collections.Generic;
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
        private Dictionary<GameObject, LevelType> _levelObjectType = new();
        private LevelConfiguration[] _levelConfigurations;
        private List<List<GameObject>> _columns = new();
        private int _currentPlayerProgress = 3;
        private int mapWidth = 10;
        
        [Inject]
        private MapIconFactory _mapIconFactory;

        [Inject]
        private LoadingScreenController _loadingScreenController;

        [Inject]
        private readonly MapSettings _mapSettings;
        
        public bool IsMapOpened { get; private set; }

        public MapModel(MapSettings mapSettings)
        {
            _mapSettings = mapSettings;
        }

        private void LoadLevels()
        {
            _mapIconFactory.Load();
            _levelConfigurations = Resources.LoadAll<LevelConfiguration>("Levels");
        }

        private void UpdateMapRelativeToProgress()
        {
            for (var colId = 0; colId < _columns.Count; colId++)
            {
                var levelIcons = _columns[colId];
                if (colId < _currentPlayerProgress)
                {
                    foreach (var levelIcon in levelIcons)
                    {
                        if (_levelObjectType[levelIcon] == LevelType.None)
                        {
                            continue;
                        }
                        levelIcon.GetComponent<Button>().interactable = false;
                        levelIcon.GetComponent<Image>().color = _mapSettings.iconColorLevelPassed;
                    }
                } else if (colId > _currentPlayerProgress)
                {
                    foreach (var levelIcon in levelIcons)
                    {
                        if (_levelObjectType[levelIcon] == LevelType.None)
                        {
                            continue;
                        }
                        levelIcon.GetComponent<Button>().enabled = false;
                        levelIcon.GetComponent<Image>().color = _mapSettings.iconColorLevelNext;
                    }
                }
                else
                {
                    foreach (var levelIcon in levelIcons)
                    {
                        if (_levelObjectType[levelIcon] == LevelType.None)
                        {
                            continue;
                        }
                        levelIcon.GetComponent<Button>().enabled = true;
                        levelIcon.GetComponent<Button>().interactable = true;
                        levelIcon.GetComponent<Image>().color = _mapSettings.iconColorLevelCurrent;
                    }
                }
            }
        }
        
        
        private void GenerateColumn(List<LevelType> row)
        {
            var column = new List<GameObject>();
            for (var index = 0; index < row.Count; index++)
            {
                var levelType = row[index];
                var button = _mapIconFactory.Create(_mapSettings.rows[index]);
                var image = button.GetComponent<Image>();
                _levelObjectType[button] = levelType;
                switch (levelType)
                {
                    case LevelType.Enemy:
                        //var randomLevel = _levelConfigurations[Random.Range(0, _levelConfigurations.Length)];
                        image.sprite = _mapSettings.enemyIcon;
                        button.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            _loadingScreenController.Open();
                            SceneManager.LoadScene("Battle");
                        });
                        break;
                    case LevelType.None:
                        //var randomLevel = _levelConfigurations[Random.Range(0, _levelConfigurations.Length)];
                        image.sprite = null;
                        image.color = new Color(0, 0, 0, 0);
                        break;
                    case LevelType.Boss:
                        image.sprite = _mapSettings.bossIcon;
                        button.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            _loadingScreenController.Open();
                            SceneManager.LoadScene("Battle");
                        });
                        break;
                }

                column.Add(button);
            }
            _columns.Add(column);
        }

        public List<List<LevelType>> GenerateLevelPipeline()
        {
            var map = new List<List<LevelType>>();

            for (var i = 0; i < mapWidth; i++)
            {
                var row = new List<LevelType>()
                {
                    LevelType.None, LevelType.None, LevelType.None,
                };

                if (i == 0)
                {
                    for (var y = 0; y < row.Count; y++)
                    {
                        row[y] = LevelType.Enemy;

                    }
                }else if (i > 0 && i < mapWidth - 1)
                {
                    var enemiesInRow = Random.Range(1, 3);
                    for (var y = 0; y < enemiesInRow; y++)
                    {
                        row[Random.Range(0, row.Count)] = LevelType.Enemy;

                    }
                }
                else if(i == mapWidth - 1)
                {
                    row[Random.Range(0, row.Count)] = LevelType.Boss;
                }
                

                map.Add(row);
            }
            
            return map;
        }

        private void GenerateViewForMap(List<List<LevelType>> levelPipeline)
        {
            foreach (var t in levelPipeline)
            {
                GenerateColumn(t);
            }
        }
        
        public void Close()
        {
            IsMapOpened = false;
            var mapViewHeight = _mapSettings.mapRectTransform.rect.height;
            _mapSettings.mapRectTransform.DOJumpAnchorPos(Vector2.zero, 2.0f, 3, 0.25f).onComplete += () =>
            {
                _mapSettings.closeButtonText.text = "Open";
            };
        }

        public void Open()
        {
            IsMapOpened = true;
            var mapViewHeight = _mapSettings.mapRectTransform.rect.height;
            _mapSettings.mapRectTransform.DOJumpAnchorPos(new Vector2(0.0f, -mapViewHeight), 2.0f, 3, 0.25f).onComplete += () =>
            {
                _mapSettings.closeButtonText.text = "Close";
            };
        }

        public void Initialize()
        {
            LoadLevels();
            var levelPipeline = GenerateLevelPipeline();
            GenerateViewForMap(levelPipeline);
            UpdateMapRelativeToProgress();
        }
    }
}
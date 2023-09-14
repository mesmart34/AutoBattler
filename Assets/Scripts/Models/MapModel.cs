using Common.Map;
using DG.Tweening;
using Factories;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Models
{
    public class MapModel : IInitializable
    {
        [Inject]
        private MapIconFactory _mapIconFactory;
        
        private readonly MapSettings _mapSettings;
        public bool IsMapOpened { get; private set; }

        public MapModel(MapSettings mapSettings)
        {
            _mapSettings = mapSettings;
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
            _mapIconFactory.Load();
            foreach (var configuration in _mapSettings.enemyBoardConfigurations)
            {
                var button = _mapIconFactory.Create(configuration, _mapSettings.rows[0]);
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("Battle");
                });
            }
        }
    }
}
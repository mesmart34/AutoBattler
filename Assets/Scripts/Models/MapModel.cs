using Common.Map;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Models
{
    public class MapModel
    {
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
    }
}
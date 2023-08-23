using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class DamagePopupFactory
    {
        private readonly DiContainer _diContainer;
        private Object _prefab;

        public DamagePopupFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _prefab = Resources.Load("Prefabs/DamagePopup");
        }
        
        public void Create(int value, Color color, Canvas canvas, float speed)
        {
            var popup = _diContainer.InstantiatePrefabForComponent<DamagePopup>(_prefab, canvas.transform);
            popup.Value = value;
            popup.ExistTime = speed;
            popup.color = color;
            popup.CanvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
        }
    }
}
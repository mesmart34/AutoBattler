using System;
using DG.Tweening;
using UnityEngine;

namespace Common.Tavern
{
    public class TavernUiController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform dragHeroesMessagePanel;

        private void Start()
        {
            dragHeroesMessagePanel.DOAnchorPosY(0, 2.0f).onComplete += () =>
            {
                dragHeroesMessagePanel.gameObject.SetActive(true);
            };
        }

        public void BeginBattle()
        {
            var rectHeight = dragHeroesMessagePanel.rect.height;
            dragHeroesMessagePanel.DOJumpAnchorPos(new Vector2(0.0f, -rectHeight), 2.0f, 3, 0.25f).onComplete += () =>
            {
                dragHeroesMessagePanel.gameObject.SetActive(false);
            };
            
        }
    }
}

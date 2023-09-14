using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Tavern
{
    public class TavernController : MonoBehaviour
    {
        [SerializeField]
        private string battleSceneName;
        
        [SerializeField]
        private RectTransform dragHeroesMessagePanel;

        [SerializeField]
        private float messagePositionY = 1.0f;
        
        [SerializeField]
        private UnityEvent onBeginPressed;

        [Inject]
        private BoardModel _boardModel;

        [SerializeField]
        private GameObject LoadScreen;

        private void Start()
        {
            var rectHeight = dragHeroesMessagePanel.rect.height;
            dragHeroesMessagePanel.gameObject.SetActive(true);
            dragHeroesMessagePanel.DOJumpAnchorPos(new Vector2(0.0f, -rectHeight - messagePositionY), 2.0f, 3, 0.5f);
        }

        public void BeginBattle()
        {
            if (_boardModel.PlayerPlatforms.Count(x => x.Busy) < 3)
            {
                return;
            }
            dragHeroesMessagePanel.DOAnchorPosY(0, 0.5f).onComplete += () =>
            {
                onBeginPressed?.Invoke();
                StartCoroutine(LoadBattleScene());
                DontDestroyOnLoad(LoadScreen);
                //dragHeroesMessagePanel.gameObject.SetActive(false);
            };
        }

        private IEnumerator LoadBattleScene()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(battleSceneName);
            asyncLoad.allowSceneActivation = false;
            var timer = 0.0f;
            while (!asyncLoad.isDone && timer < 3.0f)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            asyncLoad.allowSceneActivation = true;
        }
    }
}

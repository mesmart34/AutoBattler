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
    public class TavernUIController : IInitializable
    {
        private RectTransform _messagePanel;
        private float _messagePanelOffsetY;
        
        [Inject]
        private TavernSettings _tavernSettings;
        
        public void Initialize()
        {
            _messagePanelOffsetY = _tavernSettings.messagePositionY;
            _messagePanel = _tavernSettings.messagePanel;
        }
        
        public void OpenMessage()
        {
            var rectHeight = _messagePanel.rect.height;
            _messagePanel.gameObject.SetActive(true);
            _messagePanel.DOJumpAnchorPos(new Vector2(0.0f, -rectHeight - _tavernSettings.messagePositionY), 2.0f, 3, 0.5f);
        }

        public void CloseMessage()
        {
            _messagePanel.DOJumpAnchorPos(new Vector2(0.0f, 0), 2.0f, 3, 0.5f);
        }
        
        /*private IEnumerator LoadBattleScene()
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
        }*/
    }
}

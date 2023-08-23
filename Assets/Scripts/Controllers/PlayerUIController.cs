using DG.Tweening;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField]
        public float jumpTime = 0.25f;
        
        [SerializeField]
        public int jumpNumber = 3;
        
        [SerializeField]
        public float jumpPower = 3;
        
        [SerializeField]
        private GameObject beginAutoBattlePanel;
        
        [SerializeField]
        private GameObject battleResultScreen;

        public void ShowBeginAutoBattlePanel(string enemyName)
        {
            beginAutoBattlePanel.SetActive(true);
            var rect = beginAutoBattlePanel.GetComponent<RectTransform>();
            var startPosition = rect.anchoredPosition3D;
            var target = startPosition + new Vector3(0, rect.anchoredPosition.y - rect.sizeDelta.y, 0);
            rect.DOJumpAnchorPos(target, jumpPower, jumpNumber, jumpTime);
        }
        
        private void HideBeginAutoBattlePanel()
        {
            var rect = beginAutoBattlePanel.GetComponent<RectTransform>();
            var startPosition = rect.anchoredPosition3D;
            var target = startPosition + new Vector3(0, rect.sizeDelta.y, 0);
            rect.DOJumpAnchorPos(target, jumpPower, jumpNumber, jumpTime).onComplete = () =>
            {
                beginAutoBattlePanel.SetActive(false);
            };
        }

        public void ShowBattleEndScreen(bool win)
        {
            battleResultScreen.SetActive(true);
            var rect = battleResultScreen.GetComponent<RectTransform>();
            var startPosition = rect.anchoredPosition3D;
            var target = startPosition + new Vector3(0, rect.anchoredPosition.y - rect.sizeDelta.y, 0);
            rect.DOJumpAnchorPos(target, jumpPower, jumpNumber, jumpTime);
        }
    }
}
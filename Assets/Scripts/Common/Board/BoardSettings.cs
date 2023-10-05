using System;
using TMPro;
using UnityEngine;

namespace Common.Board
{
    [Serializable]
    public class BoardSettings
    {
        [SerializeField]
        public Transform heroPlatformParent;

        [SerializeField]
        public Transform enemyPlatformParent;

        [SerializeField]
        public Transform heroesParent;
        
        [SerializeField]
        public Transform enemyParent;
        
        [SerializeField]
        public BoardPlatformSettings boardPlatformSettings;

        [SerializeField]
        public PlayerBoardConfiguration playerBoardConfiguration;

        [SerializeField]
        public RectTransform beginBattlePanel;

        [SerializeField]
        public GameObject battleResult;
        
        [SerializeField]
        public TextMeshProUGUI battleResultText;

        [SerializeField]
        public TextMeshProUGUI enemyNameText;
    }
}
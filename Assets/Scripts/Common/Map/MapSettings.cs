using System;
using Common.Board;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Map
{
    [Serializable]
    public class MapSettings
    {
        public GameObject openCloseButton;
        public TextMeshProUGUI closeButtonText;
        public RectTransform mapRectTransform;
        public GameObject view;
        public RectTransform[] rows;
        public MapState mapState;
        
        public Sprite enemyIcon;
        public Sprite bossIcon;
        public Color iconColorLevelPassed;
        public Color iconColorLevelCurrent;
        public Color iconColorLevelNext;

        public PlayerConfiguration playerConfiguration;
    }
}
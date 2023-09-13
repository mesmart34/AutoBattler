using System;
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
    }
}
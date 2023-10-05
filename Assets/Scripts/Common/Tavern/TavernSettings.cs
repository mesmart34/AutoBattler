using System;
using System.Collections.Generic;
using Common.Board;
using Common.Hero;
using UnityEngine;

namespace Common.Tavern
{
    [Serializable]
    public class TavernSettings
    {
        [Header("Main settings")]
        public PlayerBoardConfiguration playerBoardConfiguration;
        public BoardPlatformSettings boardPlatformSettings;
        public Transform parent;
        public Transform heroesParent;
        public List<TavernHero> heroesToSpawn;
        
        [Header("UI")]
        public RectTransform messagePanel;
        public float messagePositionY;

        [Header("Unit moving")]
        public Camera camera;
        public LayerMask unitLayer;
        public LayerMask boardLayer;
        public float verticalUnitOffset;
        public float moveSpeed;
    }
}
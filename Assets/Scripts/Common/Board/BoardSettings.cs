using System;
using Common.Map;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Board
{
    [Serializable]
    public class BoardSettings
    {
        [FormerlySerializedAs("enemyBoardConfiguration")]
        [SerializeField]
        public LevelConfiguration levelConfiguration;
        
        [SerializeField]
        public PlayerBoardConfiguration playerBoardConfiguration;

        [SerializeField]
        public Camera camera;

        [SerializeField]
        public Transform transform;

        [SerializeField]
        public Vector2Int boardPlayerSideSize;

        [SerializeField]
        public Vector3 position;

        [SerializeField]
        public float spacing = 10.0f;

        [SerializeField]
        public float distanceBetweenSides = 1;

        [SerializeField]
        public bool isTavern;
    }
}
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Board
{
    [Serializable]
    public class BoardSettings
    {
        [SerializeField]
        public BoardUnitSetup boardUnitSetup;

        [SerializeField]
        public Camera camera;

        [SerializeField]
        public Transform cameraTavernPosition;

        [SerializeField]
        public Transform cameraBoardPosition;
        
        [SerializeField]
        public Vector2Int boardPlayerSideSize;

        [SerializeField]
        public Vector3 position;

        [SerializeField]
        public float spacing = 10.0f;

        [SerializeField]
        public float distanceBetweenSides = 1;
    }
}
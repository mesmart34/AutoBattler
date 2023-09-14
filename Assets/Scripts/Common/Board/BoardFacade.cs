using System;
using Models;
using UnityEngine;
using Zenject;

namespace Common.Board
{
    public class BoardFacade : MonoBehaviour
    {
        [Inject]
        private BoardModel _boardModel;

        [Inject]
        private MapModel _mapModel;

        private void Start()
        {
           // _boardModel.Initialize();
            Ready();
        }

        public void StartBattle()
        {
            _boardModel.StartBattle();
        }

        public void Ready()
        {
            _boardModel.Ready();
        }

        public void NextEnemy()
        {
            _mapModel.Open();
        }
    }
}
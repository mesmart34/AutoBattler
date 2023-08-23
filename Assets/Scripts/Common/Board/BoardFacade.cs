using Models;
using UnityEngine;
using Zenject;

namespace Common.Board
{
    public class BoardFacade : MonoBehaviour
    {
        [Inject]
        private BoardModel _boardModel;

        public void StartBattle()
        {
            _boardModel.StartBattle();
        }
    }
}
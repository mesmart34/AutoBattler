using Models;
using UnityEngine;
using Zenject;

namespace Common.Board
{
    public class BoardFacade : MonoBehaviour
    {
        [Inject]
        private BoardModel _boardModel;
        

        public void BeginBattle()
        {
            _boardModel.BeginBattle();
        }

        public void Lose()
        {
            _boardModel.Lose();
        }

        public void Win()
        {
            _boardModel.Win();
        }

        public void Next()
        {
            _boardModel.Next();
        }
    }
}
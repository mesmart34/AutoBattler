using Models;
using UnityEngine;
using Zenject;

namespace Common.Tavern
{
    public class TavernFacade : MonoBehaviour
    {
        [Inject]
        private TavernModel _tavernModel;
        
        public void BeginBattle()
        {
            _tavernModel.BeginBattle();
        }
    }
}
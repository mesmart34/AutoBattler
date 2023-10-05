using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Map
{
    public class MapFacade : MonoBehaviour
    {
        [Inject]
        private MapModel _mapModel;

        public void OnOpenCloseButtonPress()
        {
            if (_mapModel.IsMapOpened)
            {
                _mapModel.Close();
            }else
            {
                _mapModel.Open();
            }
        }
    }
}
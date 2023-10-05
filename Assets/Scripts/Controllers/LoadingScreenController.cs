using System;
using UnityEngine;

namespace Controllers
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField]
        private GameObject loadingScreen;

        [SerializeField]
        private bool autoStart;

        private void Start()
        {
            if (autoStart)
            {
                Open();
            }
        }

        public void Open()
        {
            //DontDestroyOnLoad(this);
            loadingScreen.SetActive(true);
        }

        public void Close()
        {
            loadingScreen.SetActive(false);
        }
    }
}
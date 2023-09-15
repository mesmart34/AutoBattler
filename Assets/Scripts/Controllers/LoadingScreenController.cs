using UnityEngine;

namespace Controllers
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField]
        private GameObject loadingScreen;
        
        public void Open()
        {
            DontDestroyOnLoad(this);
            loadingScreen.SetActive(true);
        }

        public void Close()
        {
            loadingScreen.SetActive(false);
        }
    }
}
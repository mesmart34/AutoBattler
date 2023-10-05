using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Common
{
    public class LoadingController : MonoBehaviour
    {
        [SerializeField]
        private string text;

        [SerializeField]
        private float speed = 1.0f;

        [SerializeField]
        private float workingTime = 1.0f;
        
        [SerializeField]
        private GameObject loadingPanel;

        [SerializeField]
        private TextMeshProUGUI loadingText;

        private void OnEnable()
        {
            StartCoroutine(TextAnimation());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator TextAnimation()
        {
            var timer = 0.0f;
            while (timer <= workingTime)
            {
                loadingText.text = text;
                for (var i = 0; i < 3; i++)
                {
                    loadingText.text += ".";
                    timer += Time.deltaTime;
                    yield return new WaitForSeconds(speed);
                }
            }
            gameObject.SetActive(false);
        }
    }
}
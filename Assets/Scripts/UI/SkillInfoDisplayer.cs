using TMPro;
using UnityEngine;

namespace UI
{
    public class SkillInfoDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _title;
        
        [SerializeField]
        private TextMeshProUGUI _info;

        public void Init(string title, string info)
        {
            _title.text = title;
            _info.text = info;
        }
    }
}
using TMPro;
using UnityEngine;

namespace StoryComics
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    [DisallowMultipleComponent]
    public class StoryComicsText : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private TextMeshProUGUI _textMeshProUGUI;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _canvasGroup.alpha = 0f;
        }

        public void SetText(string text)
            => _textMeshProUGUI.text = text;

        public CanvasGroup GetCanvasGroup()
            => _canvasGroup;
    }
}

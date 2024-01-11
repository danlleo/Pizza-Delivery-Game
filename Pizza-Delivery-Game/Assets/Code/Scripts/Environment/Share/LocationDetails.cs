using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Environment.Share
{
    public class LocationDetails : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private TextMeshProUGUI _locationNameText;
        [SerializeField] private TextMeshProUGUI _timeText;
        
        [Space(5)]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Settings")] 
        [SerializeField] private string _locationName;
        [SerializeField] private string _time;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _locationNameText.text = _locationName;
            _timeText.text = _time;
        }

        private void Start()
        {
            Animate();
        }

        private void Animate()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.AppendInterval(1f);
            mySequence.Append(_canvasGroup.DOFade(1f, 1f));
            mySequence.AppendInterval(3f);
            mySequence.Append(_canvasGroup.DOFade(0f, 1f)).OnComplete(() => Destroy(gameObject));
        }
    }
}

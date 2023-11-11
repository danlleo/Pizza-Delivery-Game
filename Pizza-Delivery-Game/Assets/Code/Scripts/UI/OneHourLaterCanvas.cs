using System.Collections;
using DG.Tweening;
using Sounds.Audio;
using UnityEngine;

namespace UI
{
    public class OneHourLaterCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private CanvasGroup _canvasGroup;
    
        [Header("Settings")]
        [SerializeField] private float _timeToFade;
        [SerializeField] private float _timeToStayBeforeFadeOut;
    
        private void Start()
        {
            _canvasGroup.DOFade(1f, _timeToFade).OnComplete(() => StartCoroutine(WaitBeforeFadeOutRoutine()));
        }

        private void FadeOut()
        {
            _canvasGroup.DOFade(0f, _timeToFade).OnComplete(() =>
            {
                BedroomAudio.Instance.PlayDoorBellSound();
            });
        }

        private IEnumerator WaitBeforeFadeOutRoutine()
        {
            yield return new WaitForSeconds(_timeToStayBeforeFadeOut);
            FadeOut();
        }
    }
}

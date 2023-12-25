using System.Collections;
using DG.Tweening;
using Environment.Bedroom;
using Sounds.Audio;
using UnityEngine;

namespace UI
{
    public class OneHourLaterCanvas : MonoBehaviour
    {
        [Header("External references")] [SerializeField]
        private CanvasGroup _canvasGroup;

        [Header("Settings")] [SerializeField] private float _timeToFade;

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
                Player.Player.Instance.PlaceAt(new Vector3(1.5f, 0.1f, 2.25f));
                BedroomCamerasTransition.Instance.ResetMainCamera();
                Destroy(gameObject);
            });
        }

        private IEnumerator WaitBeforeFadeOutRoutine()
        {
            yield return new WaitForSeconds(_timeToStayBeforeFadeOut);
            FadeOut();
        }
    }
}
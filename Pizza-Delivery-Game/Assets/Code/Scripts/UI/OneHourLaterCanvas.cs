using System.Collections;
using DG.Tweening;
using Environment.Bedroom;
using Sounds.Audio;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    [DisallowMultipleComponent]
    public class OneHourLaterCanvas : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Settings")] 
        [SerializeField] private float _timeToFade;
        [SerializeField] private float _timeToStayBeforeFadeOut;

        private Player.Player _player;
        private Canvas _canvas;
        
        private readonly Vector3 _bedPosition = new(1.5f, 0.1f, 2.25f);
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.sortingOrder = 5;
        }

        private void Start()
        {
            _player = FindObjectOfType<Player.Player>();
            _canvasGroup.DOFade(1f, _timeToFade).OnComplete(() => StartCoroutine(WaitBeforeFadeOutRoutine()));
        }

        private void FadeOut()
        {
            _canvasGroup.DOFade(0f, _timeToFade).OnComplete(() =>
            {
                BedroomAudio.Instance.PlayDoorBellSound();
                _player.transform.position = _bedPosition;
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
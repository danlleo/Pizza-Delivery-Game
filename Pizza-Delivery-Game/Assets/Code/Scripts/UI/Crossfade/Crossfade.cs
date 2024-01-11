using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Crossfade
{
    [DisallowMultipleComponent]
    public class Crossfade : MonoBehaviour, ICrossfadeService
    {
        [Header("External references")] 
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _crossfadeUI;
        
        private void Awake()
        {
            DisableUI();
        }

        private void EnableUI()
            => _crossfadeUI.gameObject.SetActive(true);

        private void DisableUI()
            => _crossfadeUI.gameObject.SetActive(false);
        
        #region FadeIn

        public void FadeIn(float duration)
        {
            _canvasGroup.DOFade(1f, duration);
        }

        public void FadeIn(Action onStart, float duration)
        {
            EnableUI();

            _canvasGroup.DOFade(1f, duration).OnStart(() => onStart?.Invoke());
        }

        public void FadeIn(Action onStart, Action onComplete, float duration)
        {
            EnableUI();

            _canvasGroup.DOFade(1f, duration).OnStart(() => onStart?.Invoke())
                .OnComplete(() => onComplete?.Invoke());
        }

        #endregion

        #region FadeOut

        public void FadeOut(float duration)
        {
            EnableUI();

            _canvasGroup.DOFade(0f, duration).OnComplete(DisableUI);
        }

        public void FadeOut(Action onStart, float duration)
        {
            EnableUI();

            _canvasGroup.DOFade(0f, duration).OnStart(() => onStart?.Invoke()).OnComplete(DisableUI);
        }

        public void FadeOut(Action onStart, Action onComplete, float duration)
        {
            EnableUI();

            _canvasGroup.DOFade(0f, duration).OnStart(() => onStart?.Invoke())
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    DisableUI();
                });
        }

        #endregion
    }
}
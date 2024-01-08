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
        
        [Header("Settings")] 
        [SerializeField] [Range(0f, 3f)] private float _crossfadeTime = 1.5f;

        private void Awake()
        {
            DisableUI();
        }

        private void EnableUI()
            => _crossfadeUI.gameObject.SetActive(true);

        private void DisableUI()
            => _crossfadeUI.gameObject.SetActive(false);
        
        #region FadeIn

        public void FadeIn()
        {
            _canvasGroup.DOFade(1f, _crossfadeTime);
        }

        public void FadeIn(Action onStart)
        {
            EnableUI();

            _canvasGroup.DOFade(1f, _crossfadeTime).OnStart(() => onStart?.Invoke());
        }

        public void FadeIn(Action onStart, Action onComplete)
        {
            EnableUI();

            _canvasGroup.DOFade(1f, _crossfadeTime).OnStart(() => onStart?.Invoke())
                .OnComplete(() => onComplete?.Invoke());
        }

        #endregion

        #region FadeOut

        public void FadeOut()
        {
            EnableUI();

            _canvasGroup.DOFade(0f, _crossfadeTime).OnComplete(DisableUI);
        }

        public void FadeOut(Action onStart)
        {
            EnableUI();

            _canvasGroup.DOFade(0f, _crossfadeTime).OnStart(() => onStart?.Invoke()).OnComplete(DisableUI);
        }

        public void FadeOut(Action onStart, Action onComplete)
        {
            EnableUI();

            _canvasGroup.DOFade(0f, _crossfadeTime).OnStart(() => onStart?.Invoke())
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    DisableUI();
                });
        }

        #endregion
    }
}
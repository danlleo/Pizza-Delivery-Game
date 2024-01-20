using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Crossfade
{
    [DisallowMultipleComponent]
    public class Crossfade : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _crossfadeUI;
        
        private void Awake()
        {
            DisableUI();
            _canvasGroup.alpha = 1f;
        }

        private void Start()
        {
            FadeOut(5f);
        }

        private void EnableUI()
            => _crossfadeUI.gameObject.SetActive(true);

        private void DisableUI()
            => _crossfadeUI.gameObject.SetActive(false);
        
        #region FadeIn

        public void FadeIn(float duration)
        {
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1f, duration).SetUpdate(this);
        }

        public void FadeIn(Action onStart, float duration)
        {
            EnableUI();

            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1f, duration).OnStart(() => onStart?.Invoke()).SetUpdate(this);
        }

        public void FadeIn(Action onStart, Action onComplete, float duration)
        {
            EnableUI();

            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1f, duration).OnStart(() => onStart?.Invoke())
                .OnComplete(() => onComplete?.Invoke()).SetUpdate(this);
        }

        #endregion

        #region FadeOut

        public void FadeOut(float duration)
        {
            EnableUI();

            _canvasGroup.DOKill();
            _canvasGroup.DOFade(0f, duration).OnComplete(DisableUI).SetUpdate(this);
        }

        public void FadeOut(Action onStart, float duration)
        {
            EnableUI();

            _canvasGroup.DOKill();
            _canvasGroup.DOFade(0f, duration).OnStart(() => onStart?.Invoke()).OnComplete(DisableUI).SetUpdate(this);
        }

        public void FadeOut(Action onStart, Action onComplete, float duration)
        {
            EnableUI();

            _canvasGroup.DOKill();
            _canvasGroup.DOFade(0f, duration).OnStart(() => onStart?.Invoke())
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    DisableUI();
                }).SetUpdate(this);
        }

        #endregion
    }
}
using System;
using DG.Tweening;
using Misc;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    public class Crossfade : Singleton<Crossfade>
    {
        [Header("External references")] [SerializeField]
        private CanvasGroup _canvasGroup;

        [Header("Settings")] [SerializeField] [Range(0f, 3f)]
        private float _crossfadeTime = 1.5f;

        #region FadeIn

        public void FadeIn()
        {
            _canvasGroup.DOFade(1f, _crossfadeTime);
        }

        public void FadeIn(Action onStart)
        {
            _canvasGroup.DOFade(1f, _crossfadeTime).OnStart(() => onStart?.Invoke());
        }

        public void FadeIn(Action onStart, Action onComplete)
        {
            _canvasGroup.DOFade(1f, _crossfadeTime).OnStart(() => onStart?.Invoke())
                .OnComplete(() => onComplete?.Invoke());
        }

        #endregion

        #region FadeOut

        public void FadeOut()
        {
            _canvasGroup.DOFade(0f, _crossfadeTime);
        }

        public void FadeOut(Action onStart)
        {
            _canvasGroup.DOFade(0f, _crossfadeTime).OnStart(() => onStart?.Invoke());
        }

        public void FadeOut(Action onStart, Action onComplete)
        {
            _canvasGroup.DOFade(0f, _crossfadeTime).OnStart(() => onStart?.Invoke())
                .OnComplete(() => onComplete?.Invoke());
        }

        #endregion
    }
}
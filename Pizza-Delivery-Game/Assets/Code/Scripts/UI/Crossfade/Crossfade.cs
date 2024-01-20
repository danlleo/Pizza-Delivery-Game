using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SetMaxAlpha();
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += SceneManager_OnActiveSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= SceneManager_OnActiveSceneChanged;
        }
        
        private void SetMaxAlpha()
        {
            _canvasGroup.alpha = 1f;
        }

        private void EnableUI()
            => _crossfadeUI.gameObject.SetActive(true);

        private void DisableUI()
            => _crossfadeUI.gameObject.SetActive(false);
        
        private void SceneManager_OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            EnableUI();
            SetMaxAlpha();
            FadeOut(5f);
        }
        
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
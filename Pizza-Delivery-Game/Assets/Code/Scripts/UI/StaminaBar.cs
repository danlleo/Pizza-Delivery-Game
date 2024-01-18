using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    [DisallowMultipleComponent]
    public class StaminaBar : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private UI _ui;
        [SerializeField] private CanvasGroup _staminaBarCanvasGroup;
        [SerializeField] private Image _foreground;

        [Header("Settings")] 
        [SerializeField] private Color _emptyStaminaColor;
        [SerializeField] private Color _fullStaminaColor;

        [SerializeField] private float _fadeInTimeInSeconds = .2f;
        [SerializeField] private float _fadeOutTimeInSeconds = .2f;
        [SerializeField] private float _delayFadeInTimeInSeconds = .2f;
        
        private Coroutine _delayFadeInRoutine;

        private bool _isFadedIn;

        private void Awake()
        {
            _isFadedIn = true;
        }

        private void OnEnable()
        {
            _ui.Player.StaminaEvent.Event += StaminaEvent;
        }

        private void OnDisable()
        {
            _ui.Player.StaminaEvent.Event -= StaminaEvent;
        }

        private void StaminaEvent(object sender, StaminaEventArgs e)
        {
            switch (e.IsStaminaFull)
            {
                case true when !_isFadedIn:
                    _delayFadeInRoutine = StartCoroutine(DelayFadeInRoutine());
                    return;
                case false when _isFadedIn:
                {
                    if (_delayFadeInRoutine != null)
                        StopCoroutine(_delayFadeInRoutine);

                    _staminaBarCanvasGroup.DOFade(0.355f, _fadeOutTimeInSeconds);
                    _isFadedIn = false;
                    break;
                }
            }

            float normalizedStaminaPercent = e.StaminaPercent / 100;

            SetForegroundFillAmount(normalizedStaminaPercent);
            SetForegroundColor(normalizedStaminaPercent);
        }

        private void SetForegroundFillAmount(float normalizedStaminaPercent)
        {
            _foreground.fillAmount = normalizedStaminaPercent;
        }

        private void SetForegroundColor(float normalizedStaminaPercent)
        {
            _foreground.color =
                Math.RemapColor(0f, .35f, _emptyStaminaColor, _fullStaminaColor, normalizedStaminaPercent);
        }

        private IEnumerator DelayFadeInRoutine()
        {
            yield return new WaitForSeconds(_delayFadeInTimeInSeconds);
            _staminaBarCanvasGroup.DOFade(0f, _fadeInTimeInSeconds);
            _isFadedIn = true;
        }
    }
}
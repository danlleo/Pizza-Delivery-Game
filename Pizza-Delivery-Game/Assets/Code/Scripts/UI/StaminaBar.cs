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
        [SerializeField] private CanvasGroup _staminaBarCanvasGroup;
        [SerializeField] private Image _foreground;
        [SerializeField] private Player.Player _player;

        [Header("Settings")] 
        [SerializeField] private Color _emptyStaminaColor; 
        [SerializeField] private Color _fullStaminaColor; 
            
        [SerializeField] private float _fadeInTimeInSeconds = .2f;
        [SerializeField] private float _fadeOutTimeInSeconds = .2f;
        [SerializeField] private float _delayFadeInTimeInSeconds = .2f;

        private bool _isFadedIn;

        private Coroutine _delayFadeInRoutine;

        private void Awake()
        {
            _isFadedIn = true;
        }

        private void OnEnable()
        {
            _player.StaminaEvent.Event += StaminaEvent;
        }

        private void OnDisable()
        {
            _player.StaminaEvent.Event -= StaminaEvent;
        }

        private void StaminaEvent(object sender, StaminaEventArgs e)
        {
            if (e.IsStaminaFull && !_isFadedIn)
            {
                _delayFadeInRoutine = StartCoroutine(DelayFadeInRoutine());
                return;
            }

            if (!e.IsStaminaFull && _isFadedIn)
            {
                if (_delayFadeInRoutine != null)
                    StopCoroutine(_delayFadeInRoutine);
                
                _staminaBarCanvasGroup.DOFade(1f, _fadeOutTimeInSeconds);
                _isFadedIn = false;
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
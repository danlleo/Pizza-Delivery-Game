using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private float _fadeInTimeInSeconds = .2f;
        [SerializeField] private float _fadeOutTimeInSeconds = .2f;

        private bool _isFadedIn;

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
                _staminaBarCanvasGroup.DOFade(0f, _fadeInTimeInSeconds);
                _isFadedIn = true;
                return;
            }

            if (!e.IsStaminaFull && _isFadedIn)
            {
                _staminaBarCanvasGroup.DOFade(1f, _fadeOutTimeInSeconds);
                _isFadedIn = false;
            }
            
            SetForegroundFillAmount(e.StaminaPercent / 100);
        }

        private void SetForegroundFillAmount(float normalizedPercent)
        {
            _foreground.fillAmount = normalizedPercent;
        }
    }
}
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private Player.Player _player;
        
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
            SetForegroundFillAmount(e.StaminaPercent / 100);
        }

        private void SetForegroundFillAmount(float normalizedPercent)
        {
            _foreground.fillAmount = normalizedPercent;
        }
    }
}
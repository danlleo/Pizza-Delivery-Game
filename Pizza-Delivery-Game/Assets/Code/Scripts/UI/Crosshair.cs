using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    public class Crosshair : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Image _crosshairImage;
        
        [Header("Settings")]
        [SerializeField] private Sprite _defaultCrosshairSprite;
        [SerializeField] private Vector2 _defaultCrosshairSize;

        [Space(5)]
        [SerializeField] private Sprite _interactCrosshairSprite;
        [SerializeField] private Vector2 _interactCrosshairSize;
        
        private Player.Player _player;

        private void OnEnable()
        {
            _player = Player.Player.Instance;
            _player.HoveringOverInteractableEvent.Event += HoveringOverInteractable_Event;
        }
        
        private void OnDisable()
        {
            _player.HoveringOverInteractableEvent.Event -= HoveringOverInteractable_Event;
        }

        private void HoveringOverInteractable_Event(object sender, HoveringOverInteractableEventArgs e)
        {
            if (e.IsInteracting)
            {
                SetInteractCrosshair();
                return;
            }
            
            SetDefaultCrosshair();
        }
        
        private void SetDefaultCrosshair()
        {
            _crosshairImage.sprite = _defaultCrosshairSprite;
            _crosshairImage.rectTransform.sizeDelta = _defaultCrosshairSize;
        }
        
        private void SetInteractCrosshair()
        {
            _crosshairImage.sprite = _interactCrosshairSprite;
            _crosshairImage.rectTransform.sizeDelta = _interactCrosshairSize;
        }
    }
}

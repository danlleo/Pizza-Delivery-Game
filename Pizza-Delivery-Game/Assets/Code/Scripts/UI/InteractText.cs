using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InteractText : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        private TextMeshProUGUI _interactText;

        private void Awake()
        {
            _interactText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
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
                SetInteractText(e.ActionDescription);
                return;
            }

            ResetInteractText();
        }
        
        private void SetInteractText(string targetText)
            => _interactText.text = $"[E] {targetText}";
        
        private void ResetInteractText()
            => _interactText.text = "";
    }
}

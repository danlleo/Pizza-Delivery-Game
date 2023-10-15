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
                _interactText.text = $"[E] {e.ActionDescription}";
                return;
            }

            _interactText.text = "";
        }
    }
}

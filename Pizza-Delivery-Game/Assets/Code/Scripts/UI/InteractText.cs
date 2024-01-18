using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [DisallowMultipleComponent]
    public class InteractText : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private UI _ui;
        
        private TextMeshProUGUI _interactText;
        private Player.Player _player;

        private void Awake()
        {
            _interactText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _ui.Player.HoveringOverInteractableEvent.Event += HoveringOverInteractable_Event;
        }

        private void OnDisable()
        {
            _ui.Player.HoveringOverInteractableEvent.Event -= HoveringOverInteractable_Event;
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
        {
            _interactText.text = $"[E] {targetText}";
        }

        private void ResetInteractText()
        {
            _interactText.text = "";
        }
    }
}
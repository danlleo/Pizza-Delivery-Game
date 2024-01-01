using EventBus;
using TMPro;
using UnityEngine;

namespace Keypad
{
    public class PasswordCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private TextMeshProUGUI _passwordText;

        private EventBinding<InteractedWithKeypadEvent> _interactedWithKeypadEventBinding;
        
        private void Awake()
        {
            HidePasswordText();
            _passwordText.text = Password.CorrectPassword;
        }

        private void OnEnable()
        {
            _interactedWithKeypadEventBinding =
                new EventBinding<InteractedWithKeypadEvent>(HandleInteractedWithKeypadEvent);
            EventBus<InteractedWithKeypadEvent>.Register(_interactedWithKeypadEventBinding);
        }

        private void OnDisable()
        {
            EventBus<InteractedWithKeypadEvent>.Deregister(_interactedWithKeypadEventBinding);
        }

        private void HidePasswordText()
            => _passwordText.enabled = false;

        private void DisplayPasswordText()
            => _passwordText.enabled = true;

        private void HandleInteractedWithKeypadEvent(InteractedWithKeypadEvent interactedWithKeypadEvent)
        {
            DisplayPasswordText();
        }
    }
}

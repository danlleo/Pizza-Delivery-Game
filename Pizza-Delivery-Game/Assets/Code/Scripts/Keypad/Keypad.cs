using EventBus;
using Interfaces;
using Misc;
using UnityEngine;

namespace Keypad
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class Keypad : MonoBehaviour, IInteractable
    {
        private const string CORRECT_PASSWORD = "673225";
        
        [Header("External References")]
        [SerializeField] private ButtonPress _buttonPress;

        private BoxCollider _keypadBoxCollider;

        private EventBinding<PasswordValidationEvent> _passwordValidationEventBinding;
        
        private void Awake()
        {
            _keypadBoxCollider = GetComponent<BoxCollider>();
            
            _buttonPress.enabled = false;
            _keypadBoxCollider.enabled = true;
        }

        private void OnEnable()
        {
            _passwordValidationEventBinding = new EventBinding<PasswordValidationEvent>(HandlePasswordValidationEvent);
            EventBus<PasswordValidationEvent>.Register(_passwordValidationEventBinding);
        }

        private void OnDisable()
        {
            EventBus<PasswordValidationEvent>.Deregister(_passwordValidationEventBinding);
        }

        public void Interact()
        {
            InputAllowance.DisableInput();
            EventBus<InteractedWithKeypadEvent>.Raise(new InteractedWithKeypadEvent());
            CrosshairDisplayStateChangedStaticEvent.Call(this, new CrosshairDisplayStateChangedEventArgs(false));
            CursorLockStateChangedStaticEvent.Call(this, new CursorLockStateChangedStaticEventArgs(false));

            _buttonPress.enabled = true;
            _keypadBoxCollider.enabled = false;
        }

        public string GetActionDescription()
        {
            return "Keypad";
        }

        private void ValidatePassword(string password)
        {
            bool isCorrect = password == CORRECT_PASSWORD;

            EventBus<PasswordValidationResponseEvent>.Raise(new PasswordValidationResponseEvent(isCorrect));
        }
        
        private void HandlePasswordValidationEvent(PasswordValidationEvent passwordValidationEvent)
        {
            ValidatePassword(passwordValidationEvent.Password);
        }
    }
}

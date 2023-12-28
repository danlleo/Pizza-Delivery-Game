using System;
using Enums.Keypad;
using EventBus;
using TMPro;
using UnityEngine;

namespace Keypad
{
    public class KeypadScreen : MonoBehaviour
    {
        private const int MAX_KEYPAD_SCREEN_TEXT_CHARACTER_LIMIT = 6;
        
        [Header("External references")]
        [SerializeField] private TextMeshProUGUI _keypadText;

        private EventBinding<DigitRegisteredEvent> _digitRegisteredEventBinding;
        private EventBinding<PasswordConfirmedEvent> _passwordConfirmedEventBinding;
        private EventBinding<PasswordValidationResponseEvent> _passwordValidationResponseEventBinding;
        
        private void Awake()
        {
            ClearKeypadText();
        }

        private void OnEnable()
        {
            _digitRegisteredEventBinding = new EventBinding<DigitRegisteredEvent>(HandleDigitRegisteredEvent);
            EventBus<DigitRegisteredEvent>.Register(_digitRegisteredEventBinding);

            _passwordConfirmedEventBinding = new EventBinding<PasswordConfirmedEvent>(HandlePasswordConfirmedEvent);
            EventBus<PasswordConfirmedEvent>.Register(_passwordConfirmedEventBinding);

            _passwordValidationResponseEventBinding =
                new EventBinding<PasswordValidationResponseEvent>(HandlePasswordValidationResponseEvent);
            EventBus<PasswordValidationResponseEvent>.Register(_passwordValidationResponseEventBinding);
        }

        private void OnDisable()
        {
            EventBus<DigitRegisteredEvent>.Deregister(_digitRegisteredEventBinding);
            EventBus<PasswordConfirmedEvent>.Deregister(_passwordConfirmedEventBinding);
            EventBus<PasswordValidationResponseEvent>.Deregister(_passwordValidationResponseEventBinding);
        }

        private void ClearKeypadText()
            => _keypadText.text = "";

        private void AddDigitToDisplay(ButtonDigit buttonDigit)
        {
            if (_keypadText.text.Length >= MAX_KEYPAD_SCREEN_TEXT_CHARACTER_LIMIT)
                return;

            char digit = buttonDigit switch
            {
                ButtonDigit.Zero => '0',
                ButtonDigit.One => '1',
                ButtonDigit.Two => '2',
                ButtonDigit.Three => '3',
                ButtonDigit.Four => '4',
                ButtonDigit.Five => '5',
                ButtonDigit.Six => '6',
                ButtonDigit.Seven => '7',
                ButtonDigit.Eight => '8',
                ButtonDigit.Nine => '9',
                _ => throw new ArgumentOutOfRangeException(nameof(buttonDigit), buttonDigit, null)
            };

            _keypadText.text += digit;
        }

        private void HandleDigitRegisteredEvent(DigitRegisteredEvent digitRegisteredEvent)
        {
            AddDigitToDisplay(digitRegisteredEvent.Digit);
        }

        private void HandlePasswordConfirmedEvent(PasswordConfirmedEvent passwordConfirmedEvent)
        {
            EventBus<PasswordValidationEvent>.Raise(new PasswordValidationEvent(_keypadText.text));
        }

        private void HandlePasswordValidationResponseEvent(
            PasswordValidationResponseEvent passwordValidationResponseEvent)
        {
            if (passwordValidationResponseEvent.IsCorrect) return;
            
            ClearKeypadText();
        }
    }
}
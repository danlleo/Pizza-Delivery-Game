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

        private EventBinding<ButtonPressedEvent> _buttonPressedEventBinding;
        
        private void Awake()
        {
            ClearKeypadText();
        }

        private void OnEnable()
        {
            _buttonPressedEventBinding = new EventBinding<ButtonPressedEvent>(HandleButtonPressedEventBinding);
            EventBus<ButtonPressedEvent>.Register(_buttonPressedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ButtonPressedEvent>.Deregister(_buttonPressedEventBinding);
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
        
        private void HandleButtonPressedEventBinding(ButtonPressedEvent buttonPressedEvent)
        {
            AddDigitToDisplay(buttonPressedEvent.Digit);
        }
    }
}
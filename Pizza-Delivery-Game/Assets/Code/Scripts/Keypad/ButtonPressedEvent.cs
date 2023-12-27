using Enums.Keypad;
using EventBus;

namespace Keypad
{
    public struct ButtonPressedEvent : IEvent
    {
        public ButtonDigit Digit;

        public ButtonPressedEvent(ButtonDigit digit)
        {
            Digit = digit;
        }
    }
}
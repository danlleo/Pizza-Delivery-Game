using Enums.Keypad;
using EventBus;

namespace Keypad
{
    public struct DigitRegisteredEvent : IEvent
    {
        public readonly ButtonDigit Digit;

        public DigitRegisteredEvent(ButtonDigit digit)
        {
            Digit = digit;
        }
    }
}
using EventBus;

namespace Keypad
{
    public struct PasswordValidationResponseEvent : IEvent
    {
        public readonly bool IsCorrect;

        public PasswordValidationResponseEvent(bool isCorrect)
        {
            IsCorrect = isCorrect;
        }
    }
}
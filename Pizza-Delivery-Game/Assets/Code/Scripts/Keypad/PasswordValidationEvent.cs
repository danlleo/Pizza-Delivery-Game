using EventBus;

namespace Keypad
{
    public struct PasswordValidationEvent : IEvent
    {
        public readonly string Password;

        public PasswordValidationEvent(string password)
        {
            Password = password;
        }
    }
}
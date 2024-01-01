using System;

namespace Keypad
{
    public static class PasswordUnknownStaticEvent
    {
        public static event EventHandler OnAnyPasswordUnknown;

        public static void Call(object sender)
        {
            OnAnyPasswordUnknown?.Invoke(sender, EventArgs.Empty);
        }
    }
}
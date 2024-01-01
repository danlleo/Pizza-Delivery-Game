using System;

namespace Keypad
{
    public static class NoFlashlightStaticEvent
    {
        public static event EventHandler OnAnyNoFlashlight;

        public static void CallNoFlashlightStaticEvent(this Keypad keypad)
        {
            OnAnyNoFlashlight?.Invoke(keypad, EventArgs.Empty);
        }
    }
}
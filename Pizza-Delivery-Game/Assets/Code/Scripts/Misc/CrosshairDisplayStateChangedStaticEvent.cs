using System;

namespace Misc
{
    public static class CrosshairDisplayStateChangedStaticEvent
    {
        public static event EventHandler<CrosshairDisplayStateChangedEventArgs> OnCursorStateChanged;

        public static void Call(object sender, CrosshairDisplayStateChangedEventArgs crosshairDisplayStateChangedEventArgs)
        {
            OnCursorStateChanged?.Invoke(sender, crosshairDisplayStateChangedEventArgs);
        }
    }

    public class CrosshairDisplayStateChangedEventArgs : EventArgs
    {
        public readonly bool IsDisplaying;

        public CrosshairDisplayStateChangedEventArgs(bool isDisplaying)
        {
            IsDisplaying = isDisplaying;
        }
    }
}
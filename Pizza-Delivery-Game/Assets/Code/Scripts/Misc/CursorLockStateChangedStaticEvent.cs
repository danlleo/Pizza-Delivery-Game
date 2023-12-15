using System;

namespace Misc
{
    public static class CursorLockStateChangedStaticEvent
    {
        public static event EventHandler<CursorLockStateChangedStaticEventArgs> OnCursorLockStatedChanged;

        public static void Call(object sender,
            CursorLockStateChangedStaticEventArgs cursorLockStateChangedStaticEventArgs)
            => OnCursorLockStatedChanged?.Invoke(sender, cursorLockStateChangedStaticEventArgs);
    }

    public class CursorLockStateChangedStaticEventArgs : EventArgs
    {
        public readonly bool IsLocked;

        public CursorLockStateChangedStaticEventArgs(bool isLocked)
        {
            IsLocked = isLocked;
        }
    }
}
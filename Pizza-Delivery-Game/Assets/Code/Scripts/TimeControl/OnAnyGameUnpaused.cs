using System;

namespace TimeControl
{
    public static class OnAnyGameUnpaused
    {
        public static event EventHandler Event;
        
        public static void Call(object sender)
            => Event?.Invoke(sender, EventArgs.Empty);
    }
}
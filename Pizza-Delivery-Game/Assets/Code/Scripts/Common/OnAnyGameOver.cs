using System;

namespace Common
{
    public static class OnAnyGameOver
    {
        public static event EventHandler Event;

        public static void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
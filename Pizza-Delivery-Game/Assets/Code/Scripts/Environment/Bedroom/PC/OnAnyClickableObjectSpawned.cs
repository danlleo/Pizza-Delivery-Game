using System;

namespace Environment.Bedroom.PC
{
    public static class OnAnyClickableObjectSpawned
    {
        public static event EventHandler Event;

        public static void Call(Clickable clickable)
        {
            Event?.Invoke(clickable, EventArgs.Empty);
        }
    }
}
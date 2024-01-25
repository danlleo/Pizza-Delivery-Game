using System;

namespace Environment.Bedroom
{
    public static class OnAnyLeftBedroom
    {
        public static event EventHandler Event;

        public static void Call(Door door)
        {
            Event?.Invoke(door, EventArgs.Empty);
        }
    }
}
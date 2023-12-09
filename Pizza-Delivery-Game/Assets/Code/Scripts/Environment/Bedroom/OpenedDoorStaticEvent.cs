using System;

namespace Environment.Bedroom
{
    public static class OpenedDoorStaticEvent
    {
        public static event EventHandler OnOpenedDoor;

        public static void Call(object sender)
        {
            OnOpenedDoor?.Invoke(sender, EventArgs.Empty);
        }
    }
}
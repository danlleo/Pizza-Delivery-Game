using System;

namespace Tablet
{
    public static class PickedUpStaticEvent
    {
        public static event EventHandler OnTabletPickedUp;
        
        public static void Call(object sender)
            => OnTabletPickedUp?.Invoke(sender, EventArgs.Empty);
    }
}
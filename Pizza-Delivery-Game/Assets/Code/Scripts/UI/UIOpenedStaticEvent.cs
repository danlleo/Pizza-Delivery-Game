using System;

namespace UI
{
    public static class UIOpenedStaticEvent
    {
        public static event EventHandler OnUIOpen;

        public static void Call(object sender)
        {
            OnUIOpen?.Invoke(sender, EventArgs.Empty);
        }
    }
}
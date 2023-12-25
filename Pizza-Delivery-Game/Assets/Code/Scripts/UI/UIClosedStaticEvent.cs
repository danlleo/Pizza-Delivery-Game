using System;

namespace UI
{
    public static class UIClosedStaticEvent
    {
        public static event EventHandler OnUIClose;

        public static void Call(object sender)
        {
            OnUIClose?.Invoke(sender, EventArgs.Empty);
        }
    }
}
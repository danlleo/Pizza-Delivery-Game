using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class NoKeycardStaticEvent
    {
        public static event EventHandler OnNoKeycardStaticEvent;

        public static void Call(object sender)
        {
            OnNoKeycardStaticEvent?.Invoke(sender, EventArgs.Empty);
        }
    }
}
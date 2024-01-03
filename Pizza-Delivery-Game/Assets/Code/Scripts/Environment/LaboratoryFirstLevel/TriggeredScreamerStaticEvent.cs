using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class TriggeredScreamerStaticEvent
    {
        public static event EventHandler OnAnyTriggeredScreamer;

        public static void Call(object sender)
        {
            OnAnyTriggeredScreamer?.Invoke(sender, EventArgs.Empty);
        }
    }
}
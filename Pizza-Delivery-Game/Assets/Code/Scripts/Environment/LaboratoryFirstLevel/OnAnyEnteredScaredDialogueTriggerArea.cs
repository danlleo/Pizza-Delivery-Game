using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class OnAnyEnteredScaredDialogueTriggerArea
    {
        public static event EventHandler Event;

        public static void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class InspectedForbiddenStaticEvent
    {
        public static event EventHandler OnAnyInspectedForbidden;

        public static void Call(object sender)
        {
            OnAnyInspectedForbidden?.Invoke(sender, EventArgs.Empty);   
        }
    }
}
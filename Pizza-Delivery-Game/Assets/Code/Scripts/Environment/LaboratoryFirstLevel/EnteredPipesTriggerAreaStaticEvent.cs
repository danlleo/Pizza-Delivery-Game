using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class EnteredPipesTriggerAreaStaticEvent
    {
        public static event EventHandler OnAnyEnteredPipesTriggerArea;
        
        public static void Call(object sender)
            => OnAnyEnteredPipesTriggerArea?.Invoke(sender, EventArgs.Empty);
    }
}

using System;

namespace Environment.LaboratoryFirstLevel
{
    public class EnteredLaboratoryEntryTriggerAreaStaticEvent
    {
        public static event EventHandler OnAnyEnteredLaboratoryEntryTriggerArea;
        
        public static void Call(object sender)
            => OnAnyEnteredLaboratoryEntryTriggerArea?.Invoke(sender, EventArgs.Empty);
    }
}
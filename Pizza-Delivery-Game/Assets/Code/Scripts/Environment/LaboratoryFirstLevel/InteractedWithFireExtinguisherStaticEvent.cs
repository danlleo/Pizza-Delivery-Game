using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class InteractedWithFireExtinguisherStaticEvent
    {
        public static event EventHandler OnAnyInteractedWithFireExtinguisher;
        
        public static void CallInteractedWithFireExtinguisherStaticEvent(this FireExtinguisher fireExtinguisher)
        {
            OnAnyInteractedWithFireExtinguisher?.Invoke(fireExtinguisher, EventArgs.Empty);
        }
    }
}
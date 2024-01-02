using System;

namespace Environment.LaboratorySecondLevel
{
    public static class NoPizzaBoxStaticEvent
    {
        public static event EventHandler OnAnyNoPizzaBox;

        public static void Call(object sender)
        {
            OnAnyNoPizzaBox?.Invoke(sender, EventArgs.Empty);
        }
    }
}
using System;

namespace Tablet
{
    public static class InteractedWithTabletStaticEvent
    {
        public static event EventHandler OnAnyInteractedWithTablet;

        public static void CallInteractedWithTabletStaticEvent(this Environment.LaboratoryFirstLevel.Tablet tablet)
        {
            OnAnyInteractedWithTablet?.Invoke(tablet, EventArgs.Empty);
        }
    }
}
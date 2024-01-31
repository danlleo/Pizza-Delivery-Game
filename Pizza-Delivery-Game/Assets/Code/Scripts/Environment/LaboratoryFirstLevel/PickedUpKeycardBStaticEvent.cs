using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class PickedUpKeycardBStaticEvent
    {
        public static event EventHandler OnAnyPickedUpKeycardB;

        public static void Call(object sender)
            => OnAnyPickedUpKeycardB?.Invoke(sender, EventArgs.Empty);
    }
}
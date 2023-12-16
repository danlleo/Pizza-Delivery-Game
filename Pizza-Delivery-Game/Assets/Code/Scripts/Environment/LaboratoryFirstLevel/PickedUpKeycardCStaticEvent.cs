using System;

namespace Environment.LaboratoryFirstLevel
{
    public class PickedUpKeycardCStaticEvent
    {
        public static event EventHandler OnAnyPickedUpKeycardC;

        public static void Call(object sender)
            => OnAnyPickedUpKeycardC?.Invoke(sender, EventArgs.Empty);
    }
}
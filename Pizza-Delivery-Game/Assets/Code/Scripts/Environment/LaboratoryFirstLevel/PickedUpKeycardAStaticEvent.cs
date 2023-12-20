using System;

namespace Environment.LaboratoryFirstLevel
{
    public class PickedUpKeycardAStaticEvent
    {
        public static event EventHandler OnAnyPickedUpKeycardA;

        public static void Call(object sender)
            => OnAnyPickedUpKeycardA?.Invoke(sender, EventArgs.Empty);
    }
}
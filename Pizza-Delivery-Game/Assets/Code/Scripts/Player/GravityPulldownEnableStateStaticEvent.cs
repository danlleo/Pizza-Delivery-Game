using System;

namespace Player
{
    public static class GravityPulldownEnableStateStaticEvent
    {
        public static event EventHandler<GravityPulldownEnableStateStaticEventArgs> OnAnyGravityPulldownEnableStateChanged;

        public static void Call(object sender,
            GravityPulldownEnableStateStaticEventArgs gravityPulldownEnableStateStaticEventArgs)
            => OnAnyGravityPulldownEnableStateChanged?.Invoke(sender, gravityPulldownEnableStateStaticEventArgs);
    }

    public class GravityPulldownEnableStateStaticEventArgs : EventArgs
    {
        public readonly bool Enabled;

        public GravityPulldownEnableStateStaticEventArgs(bool enabled)
        {
            Enabled = enabled;
        }
    }
}
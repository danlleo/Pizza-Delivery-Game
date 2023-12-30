using System;

namespace WorldScreenSpaceIcon
{
    public static class WorldScreenSpaceIconDetectedStaticEvent
    {
        public static event EventHandler<WorldScreenSpaceIconDetectedEventArgs> OnAnyWorldScreenSpaceIconDetected;

        public static void CallWorldScreenSpaceIconDetectedStaticEvent(this WorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            OnAnyWorldScreenSpaceIconDetected?.Invoke(worldScreenSpaceIcon,
                new WorldScreenSpaceIconDetectedEventArgs(worldScreenSpaceIcon));
        }
    }

    public class WorldScreenSpaceIconDetectedEventArgs : EventArgs
    {
        public readonly WorldScreenSpaceIcon WorldScreenSpaceIcon;

        public WorldScreenSpaceIconDetectedEventArgs(WorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            WorldScreenSpaceIcon = worldScreenSpaceIcon;
        }
    }
}
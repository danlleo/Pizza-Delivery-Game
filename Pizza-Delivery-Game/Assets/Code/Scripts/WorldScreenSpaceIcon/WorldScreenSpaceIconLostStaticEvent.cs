using System;

namespace WorldScreenSpaceIcon
{
    public static class WorldScreenSpaceIconLostStaticEvent
    {
        public static event EventHandler<WorldScreenSpaceIconLostEventArgs> OnAnyWorldScreenSpaceIconLost;

        public static void CallWorldScreenSpaceIconLostStaticEvent(this WorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            OnAnyWorldScreenSpaceIconLost?.Invoke(worldScreenSpaceIcon,
                new WorldScreenSpaceIconLostEventArgs(worldScreenSpaceIcon));
        }
    }

    public class WorldScreenSpaceIconLostEventArgs : EventArgs
    {
        public readonly WorldScreenSpaceIcon WorldScreenSpaceIcon;

        public WorldScreenSpaceIconLostEventArgs(WorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            WorldScreenSpaceIcon = worldScreenSpaceIcon;
        }
    }
}
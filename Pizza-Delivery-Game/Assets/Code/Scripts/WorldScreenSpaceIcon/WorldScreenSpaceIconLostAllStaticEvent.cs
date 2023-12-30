using System;

namespace WorldScreenSpaceIcon
{
    public static class WorldScreenSpaceIconLostAllStaticEvent
    {
        public static event EventHandler OnAnyWorldScreenSpaceIconLostAll;

        public static void CallWorldScreenSpaceIconLostAllStaticEvent(this WorldScreenSpaceIconDetect worldScreenSpaceIconDetect)
        {
            OnAnyWorldScreenSpaceIconLostAll?.Invoke(worldScreenSpaceIconDetect, EventArgs.Empty);
        }
    }
}
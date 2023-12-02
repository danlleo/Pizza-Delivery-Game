using System;
using UnityEngine;

namespace Door
{
    public static class DoorOpenStaticEvent
    {
        public static event EventHandler<DoorOpenStaticEventArgs> OnDoorOpened;

        public static void Call(object sender, DoorOpenStaticEventArgs doorOpenStaticEventArgs)
            => OnDoorOpened?.Invoke(sender, doorOpenStaticEventArgs);
    }

    public class DoorOpenStaticEventArgs : EventArgs
    {
        public readonly Vector3 DoorPosition;

        public DoorOpenStaticEventArgs(Vector3 doorPosition)
        {
            DoorPosition = doorPosition;
        }
    }
}
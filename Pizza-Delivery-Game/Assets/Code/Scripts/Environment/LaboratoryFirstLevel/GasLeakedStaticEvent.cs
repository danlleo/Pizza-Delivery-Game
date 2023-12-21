using System;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public static class GasLeakedStaticEvent
    {
        public static event EventHandler<GasLeakedStaticEventArgs> OnAnyGasLeaked;
        
        public static void Call(object sender, GasLeakedStaticEventArgs gasLeakedStaticEventArgs)
            => OnAnyGasLeaked?.Invoke(sender, gasLeakedStaticEventArgs);
    }

    public class GasLeakedStaticEventArgs : EventArgs
    {
        public readonly Vector3 GasLeakedPosition;

        public GasLeakedStaticEventArgs(Vector3 gasLeakedPosition)
        {
            GasLeakedPosition = gasLeakedPosition;
        }
    }
}
using System;
using Interfaces;
using UnityEngine;

namespace UI
{
    public class WorldScreenSpaceIconDetectedEvent : MonoBehaviour
    {
        public event EventHandler<WorldScreenSpaceIconDetectedEventArgs> OnWorldScreenSpaceIconDetected;
        
        public void Call(object sender, WorldScreenSpaceIconDetectedEventArgs worldScreenSpaceIconDetectedEventArgs)
            => OnWorldScreenSpaceIconDetected?.Invoke(sender, worldScreenSpaceIconDetectedEventArgs);
    }

    public class WorldScreenSpaceIconDetectedEventArgs : EventArgs
    {
        public readonly IWorldScreenSpaceIcon WorldScreenSpaceIcon;

        public WorldScreenSpaceIconDetectedEventArgs(IWorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            WorldScreenSpaceIcon = worldScreenSpaceIcon;
        }
    }
}
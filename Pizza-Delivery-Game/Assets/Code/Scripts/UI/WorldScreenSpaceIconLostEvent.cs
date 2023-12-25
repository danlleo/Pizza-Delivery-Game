using System;
using Interfaces;
using UnityEngine;

namespace UI
{
    public class WorldScreenSpaceIconLostEvent : MonoBehaviour
    {
        public event EventHandler<WorldScreenSpaceIconLostEventArgs> OnWorldScreenSpaceIconLost;

        public void Call(object sender, WorldScreenSpaceIconLostEventArgs worldScreenSpaceIconLostEventArgs)
        {
            OnWorldScreenSpaceIconLost?.Invoke(sender, worldScreenSpaceIconLostEventArgs);
        }
    }

    public class WorldScreenSpaceIconLostEventArgs : EventArgs
    {
        public readonly IWorldScreenSpaceIcon WorldScreenSpaceIcon;

        public WorldScreenSpaceIconLostEventArgs(IWorldScreenSpaceIcon worldScreenSpaceIcon)
        {
            WorldScreenSpaceIcon = worldScreenSpaceIcon;
        }
    }
}
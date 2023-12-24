using System;
using UnityEngine;

namespace UI
{
    public class WorldScreenSpaceIconLostAllEvent : MonoBehaviour
    {
        public event EventHandler OnWorldScreenSpaceIconLostAll;
        
        public void Call(object sender)
            => OnWorldScreenSpaceIconLostAll?.Invoke(sender, EventArgs.Empty);
    }
}
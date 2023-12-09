using System;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    public class OnObjectiveUpdated : MonoBehaviour
    {
        public event EventHandler Event;

        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
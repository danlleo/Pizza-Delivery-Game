using System;
using UnityEngine;

namespace UI.InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectCloseEvent : MonoBehaviour
    {
        public event EventHandler Event;

        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
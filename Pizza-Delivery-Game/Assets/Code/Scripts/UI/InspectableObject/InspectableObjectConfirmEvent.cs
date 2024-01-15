using System;
using UnityEngine;

namespace UI.InspectableObject
{
    public class InspectableObjectConfirmEvent : MonoBehaviour
    {
        public event EventHandler Event;

        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
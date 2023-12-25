using System;
using UnityEngine;

namespace UI
{
    public class ConfirmEvent : MonoBehaviour
    {
        public event EventHandler Event;

        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
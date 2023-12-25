using System;
using UnityEngine;

namespace UI.Dialogue
{
    [DisallowMultipleComponent]
    public class DialogueClosingEvent : MonoBehaviour
    {
        public event EventHandler Event;

        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
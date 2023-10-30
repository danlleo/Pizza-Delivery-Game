using System;
using Dialogue;
using Interfaces;
using UnityEngine;

namespace UI.Dialogue
{
    [DisallowMultipleComponent]
    public class DialogueOpeningEvent : MonoBehaviour, IEvent<DialogueOpeningEventArgs>
    {
        public event EventHandler<DialogueOpeningEventArgs> Event;
        
        public void Call(object sender, DialogueOpeningEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class DialogueOpeningEventArgs : EventArgs
    {
        public readonly DialogueSO Dialogue;

        public DialogueOpeningEventArgs(DialogueSO dialogue)
        {
            Dialogue = dialogue;
        }
    }
}
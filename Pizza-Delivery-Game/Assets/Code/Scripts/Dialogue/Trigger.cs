using Misc;
using UI.Dialogue;
using UnityEngine;

namespace Dialogue
{
    [DisallowMultipleComponent]
    public abstract class Trigger : Singleton<Trigger>
    {
        protected abstract UI.UI UI { get; }
        
        public virtual void Invoke(DialogueSO dialogue)
            => UI.DialogueOpeningEvent.Call(UI, new DialogueOpeningEventArgs(dialogue));
    }
}
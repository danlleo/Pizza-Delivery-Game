using Misc;
using UI.Dialogue;
using UnityEngine;

namespace Dialogue
{
    [DisallowMultipleComponent]
    public abstract class DialogueTrigger : Singleton<DialogueTrigger>
    {
        protected abstract UI.UI UI { get; }
        
        public void InvokeDialogue(DialogueSO dialogue)
            => UI.DialogueOpeningEvent.Call(UI, new DialogueOpeningEventArgs(dialogue));
    }
}
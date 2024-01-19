using UI.Dialogue;
using UnityEngine;
using Zenject;

namespace Dialogue
{
    [DisallowMultipleComponent]
    public abstract class DialogueTrigger : MonoBehaviour
    {
        private UI.UI _ui;

        [Inject]
        private void Construct(UI.UI ui)
        {
            _ui = ui;
        }

        protected void InvokeDialogue(DialogueSO dialogue)
            => _ui.DialogueOpeningEvent.Call(_ui, new DialogueOpeningEventArgs(dialogue));
    }
}
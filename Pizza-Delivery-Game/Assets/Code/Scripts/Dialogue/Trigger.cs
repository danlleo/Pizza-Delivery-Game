using Misc;
using UI.Dialogue;
using UnityEngine;

namespace Dialogue
{
    [DisallowMultipleComponent]
    public class Trigger : Singleton<Trigger>
    {
        [SerializeField] private UI.UI _ui;
        [SerializeField] private DialogueSO _dialogue;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke(_dialogue);
            }
        }

        public void Invoke(DialogueSO dialogue)
        {
            _ui.DialogueOpeningEvent.Call(_ui, new DialogueOpeningEventArgs(dialogue));
        }
    }
}

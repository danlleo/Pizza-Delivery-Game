using UnityEngine;

namespace Dialogue
{
    public abstract class DialogueActionTrigger : MonoBehaviour
    {
        private DialogueAction _currentDialogueAction;
        
        public abstract void OnEnable();

        public abstract void OnDisable();

        protected void Perform(DialogueAction dialogueAction)
        {
            _currentDialogueAction = Instantiate(dialogueAction);
            _currentDialogueAction.Perform();
        }
    }
}

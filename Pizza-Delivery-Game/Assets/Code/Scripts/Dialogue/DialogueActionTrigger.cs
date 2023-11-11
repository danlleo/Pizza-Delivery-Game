using UnityEngine;

namespace Dialogue
{
    public abstract class DialogueActionTrigger : MonoBehaviour
    {
        public abstract void OnEnable();

        public abstract void OnDisable();

        protected void Perform(DialogueAction dialogueAction)
        {
            
        }
    }
}

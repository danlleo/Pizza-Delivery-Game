using Misc;
using UnityEngine;

namespace Dialogue
{
    [DisallowMultipleComponent]
    public class Trigger : Singleton<Trigger>
    {
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
            TriggeredStaticEvent.Call(this, new DialogueTriggeredEventArgs(dialogue));
        }
    }
}

using System;
using UnityEngine;
using Ink.Runtime;

namespace Dialogue
{
    public class Reader : MonoBehaviour
    {
        
        
        private bool _isReading;
        
        private void OnEnable()
        {
            TriggeredStaticEvent.OnDialogueTriggered += TriggeredStatic_Event;
        }

        private void OnDisable()
        {
            TriggeredStaticEvent.OnDialogueTriggered -= TriggeredStatic_Event;
        }

        private void TriggeredStatic_Event(object sender, DialogueTriggeredEventArgs e)
        {
            ReadDialogue(e.Dialogue);
        }

        private void ReadDialogue(DialogueSO dialogue)
        {
            if (_isReading) return;

            _isReading = true;
        }
        
        
    }
}

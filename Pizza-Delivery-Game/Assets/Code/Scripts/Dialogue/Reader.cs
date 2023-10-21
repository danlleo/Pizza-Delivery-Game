using System;
using UnityEngine;
using Ink.Runtime;
using TMPro;

namespace Dialogue
{
    public class Reader : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private GameObject _dialogueContainer;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        [Header("Settings")] 
        [SerializeField] private float _characterTimeToPrintInSeconds;
        [SerializeField] private float _waitTimeToMoveToNextLineInSeconds;
        
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

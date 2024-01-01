using System;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class Outdoor : DialogueTrigger
    {
        [Header("External references")]
        [SerializeField] private UI.UI _ui;
        [SerializeField] private Scientist.Scientist _scientist;
        [SerializeField] private DialogueSO _scientistGreetingsDialogueSO;
        
        protected override UI.UI UI => _ui;

        private void OnEnable()
        {
            _scientist.StartedTalkingEvent.Event += StartedTalking_Event;
        }

        private void OnDisable()
        {
            _scientist.StartedTalkingEvent.Event -= StartedTalking_Event;
        }

        private void StartedTalking_Event(object sender, EventArgs e)
        {
            InvokeDialogue(_scientistGreetingsDialogueSO);
        }
    }
}

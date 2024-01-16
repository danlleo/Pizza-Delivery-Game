using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class Bedroom : DialogueTrigger
    {
        [SerializeField] private UI.UI _ui;
        [SerializeField] private DialogueSO _sentApplicationDialogueSO; 
        
        protected override UI.UI UI => _ui;

        private void OnEnable()
        {
            OnAnyStoppedUsingPC.Event += StoppedUsingPCStaticEventEvent;
        }

        private void OnDisable()
        {
            OnAnyStoppedUsingPC.Event -= StoppedUsingPCStaticEventEvent;
        }

        private void StoppedUsingPCStaticEventEvent(object sender, EventArgs e)
        {
            InvokeDialogue(_sentApplicationDialogueSO);
        }
    }
}

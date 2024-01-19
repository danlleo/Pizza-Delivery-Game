using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class Bedroom : DialogueTrigger
    {
        [Header("Dialogue items")] 
        [SerializeField] private DialogueSO _sentApplicationDialogueSO; 

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

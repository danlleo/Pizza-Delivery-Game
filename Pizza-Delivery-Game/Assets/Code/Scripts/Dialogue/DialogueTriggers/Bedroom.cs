using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class Bedroom : DialogueTrigger
    {
        [Header("Dialogue items")] 
        [SerializeField] private DialogueSO _sentApplicationDialogueSO;
        [SerializeField] private DialogueSO _browserPopUpsAppearedDialogueSO;

        private void OnEnable()
        {
            Environment.Bedroom.PC.OnAnyPopUpsAppeared.Event += OnAnyPopUpsAppeared;
            OnAnyStoppedUsingPC.Event += StoppedUsingPCStaticEventEvent;
        }

        private void OnDisable()
        {
            Environment.Bedroom.PC.OnAnyPopUpsAppeared.Event -= OnAnyPopUpsAppeared;
            OnAnyStoppedUsingPC.Event -= StoppedUsingPCStaticEventEvent;
        }

        private void StoppedUsingPCStaticEventEvent(object sender, EventArgs e)
        {
            InvokeDialogue(_sentApplicationDialogueSO);
        }
        
        private void OnAnyPopUpsAppeared(object sender, EventArgs e)
        {
            InvokeDialogue(_browserPopUpsAppearedDialogueSO);
        }
    }
}

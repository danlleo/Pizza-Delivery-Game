using System;
using System.Collections;
using Environment.Bedroom;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class BedroomDialogueTrigger : DialogueTrigger
    {
        [Header("Dialogue items")] 
        [SerializeField] private DialogueSO _sentApplicationDialogueSO;
        [SerializeField] private DialogueSO _browserPopUpsAppearedDialogueSO;
        [SerializeField] private DialogueSO _doorBellDialogueSO;
        [SerializeField] private DialogueSO _bedroomStartDialogueSO;
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            InvokeDialogue(_bedroomStartDialogueSO);
        }

        private void OnEnable()
        {
            Environment.Bedroom.PC.OnAnyPopUpsAppeared.Event += OnAnyPopUpsAppeared;
            OnAnyStoppedUsingPC.Event += StoppedUsingPCStaticEventEvent;
            WokeUpStaticEvent.OnWokeUp += OnAnyWokeUp;
        }
        
        private void OnDisable()
        {
            Environment.Bedroom.PC.OnAnyPopUpsAppeared.Event -= OnAnyPopUpsAppeared;
            OnAnyStoppedUsingPC.Event -= StoppedUsingPCStaticEventEvent;
            WokeUpStaticEvent.OnWokeUp -= OnAnyWokeUp;
        }

        private void StoppedUsingPCStaticEventEvent(object sender, EventArgs e)
        {
            InvokeDialogue(_sentApplicationDialogueSO);
        }
        
        private void OnAnyPopUpsAppeared(object sender, EventArgs e)
        {
            InvokeDialogue(_browserPopUpsAppearedDialogueSO);
        }
        
        private void OnAnyWokeUp(object sender, EventArgs e)
        {
            InvokeDialogue(_doorBellDialogueSO);
        }
    }
}

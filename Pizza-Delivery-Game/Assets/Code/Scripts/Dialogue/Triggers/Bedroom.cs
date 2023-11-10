using System;
using Environment.Bedroom.PC;
using UnityEngine;

namespace Dialogue.Triggers
{
    public class Bedroom : Trigger
    {
        [SerializeField] private UI.UI _ui;
        [SerializeField] private DialogueSO _sentApplicationDialogueSO; 
            
        protected override UI.UI UI => _ui;

        private void OnEnable()
        {
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEvent_OnEnded;
        }

        private void OnDisable()
        {
            StoppedUsingPCStaticEvent.OnEnded -= StoppedUsingPCStaticEvent_OnEnded;
        }

        private void StoppedUsingPCStaticEvent_OnEnded(object sender, EventArgs e)
        {
            Invoke(_sentApplicationDialogueSO);
        }
    }
}

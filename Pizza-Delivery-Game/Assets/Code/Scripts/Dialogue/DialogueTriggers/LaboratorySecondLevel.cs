using System;
using Environment.LaboratorySecondLevel;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class LaboratorySecondLevel : DialogueTrigger
    {
        [Header("Dialogue items")] 
        [SerializeField] private DialogueSO _noPizzaBoxDialogueSO;
        
        private void OnEnable()
        {
            NoPizzaBoxStaticEvent.OnAnyNoPizzaBox += OnAnyNoPizzaBox;
        }

        private void OnDisable()
        {
            NoPizzaBoxStaticEvent.OnAnyNoPizzaBox -= OnAnyNoPizzaBox;
        }

        private void OnAnyNoPizzaBox(object sender, EventArgs e)
        {
            InvokeDialogue(_noPizzaBoxDialogueSO);
        }
    }
}

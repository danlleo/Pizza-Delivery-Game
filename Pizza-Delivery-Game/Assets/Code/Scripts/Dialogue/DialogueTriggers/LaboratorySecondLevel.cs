using System;
using Environment.LaboratorySecondLevel;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class LaboratorySecondLevel : DialogueTrigger
    {
        [Header("External references")]
        [SerializeField] private UI.UI _ui;

        [Header("Dialogue items")] 
        [SerializeField] private DialogueSO _noPizzaBoxDialogueSO;
        
        protected override UI.UI UI => _ui;

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

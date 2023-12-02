using System;
using Environment.LaboratoryFirstLevel;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class LaboratoryFirstLevel : DialogueTrigger
    {
        [SerializeField] private UI.UI _ui;
        [SerializeField] private DialogueSO _noKeycardDialogueSO;
        
        protected override UI.UI UI => _ui;

        private void OnEnable()
        {
            KeycardStateStaticEvent.OnKeycardStateChanged += KeycardEvent;
        }

        private void OnDisable()
        {
            KeycardStateStaticEvent.OnKeycardStateChanged -= KeycardEvent;
        }

        private void KeycardEvent(object sender, EventArgs e)
        {
            Invoke(_noKeycardDialogueSO);
        }
    }
}

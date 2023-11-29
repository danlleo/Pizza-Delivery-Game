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
            NoKeycardStaticEvent.OnNoKeycardStaticEvent += NoKeycard_Event;
        }

        private void OnDisable()
        {
            NoKeycardStaticEvent.OnNoKeycardStaticEvent -= NoKeycard_Event;
        }

        private void NoKeycard_Event(object sender, EventArgs e)
        {
            Invoke(_noKeycardDialogueSO);
        }
    }
}

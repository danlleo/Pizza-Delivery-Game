using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class LaboratoryEntry : DialogueTrigger
    {
        [SerializeField] private UI.UI _ui;
        [SerializeField] private DialogueSO _finishedIntroductionDialogueSO;
        [SerializeField] private DialogueSO _openedDoorDialogueSO;
        
        protected override UI.UI UI => _ui;
    }
}

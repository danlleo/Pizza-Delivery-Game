using Environment.LaboratoryFirstLevel;
using EventBus;
using UnityEngine;

namespace Dialogue.DialogueTriggers
{
    public class LaboratoryFirstLevel : DialogueTrigger
    {
        [SerializeField] private UI.UI _ui;
        [SerializeField] private DialogueSO _noKeycardDialogueSO;
        [SerializeField] private DialogueSO _noWrenchDialogueSO;
        [SerializeField] private DialogueSO _fixedPipesDialogueSO;
        
        protected override UI.UI UI => _ui;

        private EventBinding<FixPipesEvent> _noWrenchEventBinding;
        
        private void OnEnable()
        {
            KeycardStateStaticEvent.OnKeycardStateChanged += OnAnyKeycardStateChanged;

            _noWrenchEventBinding = new EventBinding<FixPipesEvent>(Player_OnPipeFix);
            EventBus<FixPipesEvent>.Register(_noWrenchEventBinding);
        }

        private void OnDisable()
        {
            KeycardStateStaticEvent.OnKeycardStateChanged -= OnAnyKeycardStateChanged;
            EventBus<FixPipesEvent>.Deregister(_noWrenchEventBinding);
        }

        private void Player_OnPipeFix(FixPipesEvent fixPipesEvent)
        {
            if (!fixPipesEvent.HasFixed)
            {
                Invoke(_noWrenchDialogueSO);
                return;
            }
            
            Invoke(_fixedPipesDialogueSO);
        }

        private void OnAnyKeycardStateChanged(object sender, KeycardStateStaticEventArgs e)
        {
            if (e.AccessGranted) return;
            
            Invoke(_noKeycardDialogueSO);
        }
    }
}

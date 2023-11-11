namespace Dialogue.DialogueActionTriggers
{
    public class Bedroom : DialogueActionTrigger
    {
        private DialogueAction _currentDialogueAction;

        public override void OnEnable()
        {
            DialogueActionStaticEvent.OnDialogueAction += DialogueActionStaticEvent_OnDialogueAction;
        }

        public override void OnDisable()
        {
            DialogueActionStaticEvent.OnDialogueAction -= DialogueActionStaticEvent_OnDialogueAction;
        }
        
        private void DialogueActionStaticEvent_OnDialogueAction(object sender, DialogueActionStaticEventArgs e)
        {
            Perform(e.DialogueAction);
        }
    }
}

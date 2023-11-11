using System;

namespace Dialogue
{
    public static class DialogueActionStaticEvent
    {
        public static event EventHandler<DialogueActionStaticEventArgs> OnDialogueAction;

        public static void Call(object sender, DialogueActionStaticEventArgs eventArgs)
        {
            OnDialogueAction?.Invoke(sender, eventArgs);
        }
    }

    public class DialogueActionStaticEventArgs : EventArgs
    {
        public DialogueAction DialogueAction;

        public DialogueActionStaticEventArgs(DialogueAction dialogueAction)
        {
            DialogueAction = dialogueAction;
        }
    }
}
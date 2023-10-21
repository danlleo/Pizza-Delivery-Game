using System;
using Dialogue;

public static class TriggeredStaticEvent
{
    public static event EventHandler<DialogueTriggeredEventArgs> OnDialogueTriggered;
    
    public static void Call(object sender, DialogueTriggeredEventArgs eventArgs)
    {
        OnDialogueTriggered?.Invoke(sender, eventArgs);
    }
}

public class DialogueTriggeredEventArgs : EventArgs
{
    public readonly DialogueSO Dialogue;

    public DialogueTriggeredEventArgs(DialogueSO dialogue)
    {
        Dialogue = dialogue;
    }
}

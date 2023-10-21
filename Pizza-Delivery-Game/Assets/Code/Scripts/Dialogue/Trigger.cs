using Misc;

namespace Dialogue
{
    public class Trigger : Singleton<Trigger>
    {
        public void Invoke(DialogueSO dialogue)
        {
            TriggeredStaticEvent.Call(this, new DialogueTriggeredEventArgs(dialogue));
        }
    }
}

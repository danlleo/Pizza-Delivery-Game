using Scientist;

namespace Dialogue.ConcreteActions
{
    public class WalkToDoor : DialogueAction
    {
        protected override void Perform()
        {
            FinishedTalkingStaticEvent.Call(this);
        }
    }
}

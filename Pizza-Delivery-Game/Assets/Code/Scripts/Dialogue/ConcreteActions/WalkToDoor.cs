using Scientist;

namespace Dialogue.ConcreteActions
{
    public class WalkToDoor : DialogueAction
    {
        public override void Perform()
        {
            FinishedTalkingStaticEvent.Call(this);
        }
    }
}

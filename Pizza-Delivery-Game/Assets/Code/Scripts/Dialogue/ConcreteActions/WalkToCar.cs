using Scientist;

namespace Dialogue.ConcreteActions
{
    public class WalkToCar : DialogueAction
    {
        public override void Perform()
        {
            FinishedTalkingStaticEvent.Call(this);
        }
    }
}

using Scientist;

namespace Dialogue.ConcreteActions
{
    public class WalkToCar : DialogueAction
    {
        protected override void Perform()
        {
            FinishedTalkingStaticEvent.Call(this);
        }
    }
}

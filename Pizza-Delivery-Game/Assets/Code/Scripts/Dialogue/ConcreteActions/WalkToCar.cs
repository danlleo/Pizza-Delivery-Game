using Scientist;
using Scientist.Outdoor;

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

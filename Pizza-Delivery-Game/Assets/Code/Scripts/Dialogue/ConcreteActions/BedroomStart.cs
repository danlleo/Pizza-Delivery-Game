using Environment.Bedroom.PC;

namespace Dialogue.ConcreteActions
{
    public class BedroomStart : DialogueAction
    {
        protected override void Perform()
        {
            OnAnyAllowedUsingPC.Call(this);
        }
    }
}

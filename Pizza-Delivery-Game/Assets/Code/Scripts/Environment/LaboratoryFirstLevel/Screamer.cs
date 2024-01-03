using Dialogue;
using Misc;
using UnityEngine;
using Utilities;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Screamer : DialogueAction
    {
        protected override void Perform()
        {
            Test();
        }
        
        private void Test()
        {
            var spookRestAction = new RestAction(this);

            spookRestAction
                .AddChain(() =>
                {
                    CrosshairDisplayStateChangedStaticEvent.Call(this,
                        new CrosshairDisplayStateChangedEventArgs(false));
                    InputAllowance.DisableInput();
                })
                .AddChain(() =>
                {
                    print("Second test action");
                });
            
            spookRestAction.Execute();
        }
    }
}

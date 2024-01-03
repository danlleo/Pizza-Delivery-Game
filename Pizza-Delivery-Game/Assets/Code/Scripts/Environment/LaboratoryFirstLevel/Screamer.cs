using Dialogue;
using Misc;
using UnityEngine;
using Utilities;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Screamer : DialogueAction
    {
        private void Test()
        {
            var spookRestAction = new RestAction(this);

            spookRestAction
                .AddChain(() =>
                {
                    InputAllowance.DisableInput();
                    
                    print(Player.Player.Instance);
                    
                    CrosshairDisplayStateChangedStaticEvent.Call(Player.Player.Instance,
                        new CrosshairDisplayStateChangedEventArgs(false));
                })
                .AddChain(() =>
                {
                    print("Test display");
                });

            spookRestAction.Execute();
        }

        public override void Perform()
        {
            Test();
        }
    }
}

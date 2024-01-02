using Misc;
using UnityEngine;
using Utilities;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Screamer : MonoBehaviour
    {
        [SerializeField] private Canvas _screamerCanvas;
        
        public void Spook()
        {
            var spookRestAction = new RestAction(this);

            spookRestAction
                .AddChain(() =>
                {
                    InputAllowance.DisableInput();
                    CrosshairDisplayStateChangedStaticEvent.Call(Player.Player.Instance,
                        new CrosshairDisplayStateChangedEventArgs(false));
                }, 2f)
                .AddChain(() =>
                {
                    print("Test display");
                });
            
            spookRestAction.Execute();
        }
    }
}

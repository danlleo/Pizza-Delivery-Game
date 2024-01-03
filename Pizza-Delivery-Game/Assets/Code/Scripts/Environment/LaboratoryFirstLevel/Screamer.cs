using Dialogue;
using Misc;
using UnityEngine;
using Utilities;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Screamer : DialogueAction
    {
        [Header("External references")]
        [SerializeField] private Canvas _screamerCanvas;
        
        protected override void Perform()
        {
            Spook();
        }
        
        private void Spook()
        {
            var spookRestAction = new RestAction(this);
            Canvas screamerCanvas = null;
            
            spookRestAction
                .AddChain(() => InspectedForbiddenStaticEvent.Call(this))
                .AddChain(() =>
                {
                    TriggeredScreamerStaticEvent.Call(this);
                    CrosshairDisplayStateChangedStaticEvent.Call(this,
                        new CrosshairDisplayStateChangedEventArgs(false));
                    InputAllowance.DisableInput();
                    screamerCanvas = Instantiate(_screamerCanvas);
                }, 2f)
                .AddChain(() =>
                {
                    Destroy(screamerCanvas.gameObject);
                    CrosshairDisplayStateChangedStaticEvent.Call(this,
                        new CrosshairDisplayStateChangedEventArgs(true));
                    InputAllowance.EnableInput();
                }, 0.65f);
            
            spookRestAction.Execute();
        }
    }
}

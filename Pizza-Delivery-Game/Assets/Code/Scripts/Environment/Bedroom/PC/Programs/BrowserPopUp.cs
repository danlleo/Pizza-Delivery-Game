using System;

namespace Environment.Bedroom.PC.Programs
{
    public class BrowserPopUp : Clickable
    {
        public static event EventHandler OnAnyClosedPopUp;
        
        public void Initialize()
        {
            OnAnyClickableObjectSpawned.Call(this);
        }
        
        protected override float DelayTimeInSeconds => 0.1f;
        
        protected override void PerformAction()
        {
            OnAnyClosedPopUp?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}

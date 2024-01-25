using UnityEngine;

namespace Environment.Bedroom.PC.Programs
{
    [DisallowMultipleComponent]
    public class BrowserJobWebSite : Clickable
    {
        protected override float DelayTimeInSeconds => 0.1f;
        
        protected override void PerformAction()
        {
            OnAnyJobReviewing.Call(this);
            Destroy(this);
        }
        
        public void Initialize()
        {
            OnAnyClickableObjectSpawned.Call(this);
        }
    }
}
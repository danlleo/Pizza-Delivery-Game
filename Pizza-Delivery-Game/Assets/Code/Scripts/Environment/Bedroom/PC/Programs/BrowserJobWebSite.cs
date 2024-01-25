using UnityEngine;

namespace Environment.Bedroom.PC.Programs
{
    [DisallowMultipleComponent]
    public class BrowserJobWebSite : Clickable
    {
        protected override float DelayTimeInSeconds => 0.1f;
        
        protected override void PerformAction()
        {
            // TODO: Open canvas window describing the job
        }
        
        public void Initialize()
        {
            OnAnyClickableObjectSpawned.Call(this);
        }
    }
}
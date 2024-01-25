using System;

namespace Environment.Bedroom.PC.Programs
{
    public class Browser : Clickable
    {
        public static event EventHandler OnAnyOpenedBrowser;
		
        protected override float DelayTimeInSeconds => 1f;

        protected override void PerformAction()
        {
	        OnAnyOpenedBrowser?.Invoke(this, EventArgs.Empty);
            Destroy(this);
        }
    }
}

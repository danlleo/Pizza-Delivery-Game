namespace Environment.Bedroom.PC.Programs
{
    public class Note : Clickable
    {
        protected override float DelayTimeInSeconds { get; set; } = 0.5f;
        
        protected override void PerformAction()
        {
            OnAnyStoppedUsingPC.Call(this);
            Destroy(gameObject);
        }
    }
}

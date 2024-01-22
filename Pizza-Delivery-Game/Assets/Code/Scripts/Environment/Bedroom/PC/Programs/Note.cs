namespace Environment.Bedroom.PC.Programs
{
    public class Note : Clickable
    {
        protected override float DelayTimeInSeconds => 0.5f;

        protected override void PerformAction()
        {
            OnAnyStoppedUsingPC.Call(this);
            Destroy(gameObject);
        }
    }
}

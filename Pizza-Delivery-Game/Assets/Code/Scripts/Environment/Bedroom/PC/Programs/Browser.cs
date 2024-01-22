namespace Environment.Bedroom.PC.Programs
{
    public class Browser : Clickable
    {
        protected override float DelayTimeInSeconds => 1f;

        protected override void PerformAction()
        {
            Destroy(gameObject);
        }
    }
}

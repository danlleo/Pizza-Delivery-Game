namespace Environment.Bedroom.PC.Programs
{
    public class Browser : Clickable
    {
        protected override float DelayTimeInSeconds { get; set; } = 1f;

        protected override void PerformAction()
        {
            Destroy(gameObject);
        }
    }
}

namespace Environment.Bedroom.PC.Programs
{
    public class Browser : Clickable
    {
        public override void HandleClick()
        {
            Destroy(gameObject);
        }
    }
}

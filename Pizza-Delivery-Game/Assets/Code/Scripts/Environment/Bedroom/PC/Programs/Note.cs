namespace Environment.Bedroom.PC.Programs
{
    public class Note : Clickable
    {
        public override void HandleClick()
        {
            OnAnyStoppedUsingPC.Call(this);
            Destroy(gameObject);
        }
    }
}

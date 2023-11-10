namespace Environment.Bedroom.PC.Programs
{
    public class Note : Clickable
    {
        public override void HandleClick()
        {
            StoppedUsingPCStaticEvent.Call(Player.Player.Instance);
            Destroy(gameObject);
        }
    }
}

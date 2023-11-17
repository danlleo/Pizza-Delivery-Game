namespace Environment.Bedroom.PC.Programs
{
    public class Note : Clickable
    {
        public override void HandleClick()
        {
            Player.Player.Instance.CallStoppedUsingPC();
            Destroy(gameObject);
        }
    }
}

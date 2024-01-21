using System;

namespace DataPersistence
{
    public static class SaveStaticEvent
    {
        public static EventHandler OnSave;
        
        public static void Call(object sender)
            => OnSave?.Invoke(sender, EventArgs.Empty);
    }
}
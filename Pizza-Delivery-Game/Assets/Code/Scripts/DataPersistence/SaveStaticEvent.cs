using System;

namespace DataPersistence
{
    public static class SaveStaticEvent
    {
        public static EventHandler OnSave;
        
        public static void CallSaveEvent(object sender)
            => OnSave?.Invoke(sender, EventArgs.Empty);
    }
}
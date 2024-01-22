using System;

namespace Settings
{
    public static class OnAnySettingsChanged
    {
        public static event EventHandler<OnAnySettingsChangedArgs> Event;

        public static void Call(object sender, OnAnySettingsChangedArgs onAnySettingsChangedArgs)
        {
            Event?.Invoke(sender, onAnySettingsChangedArgs);
        }
    }

    public class OnAnySettingsChangedArgs : EventArgs
    {
        public readonly float MouseSensitivity;
        public readonly bool VSyncEnabled;

        public OnAnySettingsChangedArgs(float mouseSensitivity, bool vSyncEnabled)
        {
            MouseSensitivity = mouseSensitivity;
            VSyncEnabled = vSyncEnabled;
        }
    }
}
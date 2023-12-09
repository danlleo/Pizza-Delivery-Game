using System;

namespace UI.Objective
{
    public static class ToggleObjectiveWindowStaticEvent
    {
        public static event EventHandler<ToggleObjectiveWindowStaticEventArgs> OnObjectiveWindowToggleChanged;

        public static void Call(object sender,
            ToggleObjectiveWindowStaticEventArgs toggleObjectiveWindowStaticEventArgs)
        {
            OnObjectiveWindowToggleChanged?.Invoke(sender, toggleObjectiveWindowStaticEventArgs);
        }
    }

    public class ToggleObjectiveWindowStaticEventArgs : EventArgs
    {
        public readonly bool IsOpen;

        public ToggleObjectiveWindowStaticEventArgs(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}
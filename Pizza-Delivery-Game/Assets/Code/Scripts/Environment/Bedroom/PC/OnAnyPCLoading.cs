using System;

namespace Environment.Bedroom.PC
{
    public static class OnAnyPCLoading
    {
        public static event EventHandler<OnAnyPCLoadingEventArgs> Event;

        public static void Call(Clickable clickable, OnAnyPCLoadingEventArgs onAnyPCLoadingEventArgs)
        {
            Event?.Invoke(clickable, onAnyPCLoadingEventArgs);
        }
    }

    public class OnAnyPCLoadingEventArgs : EventArgs
    {
        public readonly bool IsLoading;

        public OnAnyPCLoadingEventArgs(bool isLoading)
        {
            IsLoading = isLoading;
        }
    }
}
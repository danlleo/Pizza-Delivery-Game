using System;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    public static class KeycardStateStaticEvent
    {
        public static event EventHandler<KeycardStateStaticEventArgs> OnKeycardStateChanged;

        public static void Call(object sender, KeycardStateStaticEventArgs keycardStateStaticEventArgs)
        {
            OnKeycardStateChanged?.Invoke(sender, keycardStateStaticEventArgs);
        }
    }

    public class KeycardStateStaticEventArgs : EventArgs
    {
        public readonly bool AccessGranted;
        public readonly Vector3 TerminalPosition;

        public KeycardStateStaticEventArgs(bool accessGranted, Vector3 terminalPosition)
        {
            AccessGranted = accessGranted;
            TerminalPosition = terminalPosition;
        }
    }
}
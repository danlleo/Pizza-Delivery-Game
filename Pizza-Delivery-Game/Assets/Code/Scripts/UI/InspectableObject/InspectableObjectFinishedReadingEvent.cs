﻿using System;
using UnityEngine;

namespace UI.InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectFinishedReadingEvent : MonoBehaviour
    {
        public event EventHandler<InspectableObjectFinishedReadingEventArgs> Event;

        public void Call(object sender, InspectableObjectFinishedReadingEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class InspectableObjectFinishedReadingEventArgs : EventArgs
    {
        public readonly bool CanClose;

        public InspectableObjectFinishedReadingEventArgs(bool canClose)
        {
            CanClose = canClose;
        }
    }
}
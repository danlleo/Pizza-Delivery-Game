﻿using System;
using InspectableObject;
using Interfaces;
using UnityEngine;

namespace UI.InspectableObject
{
    [DisallowMultipleComponent]
    public class InspectableObjectOpeningEvent : MonoBehaviour, IEvent<InspectableObjectOpeningEventArgs>
    {
        public event EventHandler<InspectableObjectOpeningEventArgs> Event;
        
        public void Call(object sender, InspectableObjectOpeningEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class InspectableObjectOpeningEventArgs : EventArgs
    {
        public readonly InspectableObjectSO InspectableObject;
        public Action OnComplete;

        public InspectableObjectOpeningEventArgs(InspectableObjectSO inspectableObject, Action onComplete)
        {
            InspectableObject = inspectableObject;
            OnComplete = onComplete;
        }
    }
}
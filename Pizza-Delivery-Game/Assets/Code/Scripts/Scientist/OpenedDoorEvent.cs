﻿using System;
using Interfaces;
using UnityEngine;

namespace Scientist
{
    public class OpenedDoorEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
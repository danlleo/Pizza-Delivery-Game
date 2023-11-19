﻿using System;
using Interfaces;
using UnityEngine;

namespace Monster
{
    public class DetectedTargetEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;
        
        public void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
﻿using System;

namespace Environment.Bedroom.PC
{
    public static class OnAnyAllowedUsingPC
    {
        public static event EventHandler Event;

        public static void Call(object sender)
        {
            Event?.Invoke(sender, EventArgs.Empty);
        }
    }
}
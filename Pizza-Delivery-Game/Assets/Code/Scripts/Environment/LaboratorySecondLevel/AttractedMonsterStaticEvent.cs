using System;
using UnityEngine;

namespace Environment.LaboratorySecondLevel
{
    public static class AttractedMonsterStaticEvent
    {
        public static event EventHandler<AttractedMonsterEventArgs> OnAnyAttractedMonster;

        public static void Call(object sender, AttractedMonsterEventArgs attractedMonsterEventArgs)
        {
            OnAnyAttractedMonster?.Invoke(sender, attractedMonsterEventArgs);
        }
    }

    public class AttractedMonsterEventArgs : EventArgs
    {
        public readonly Vector3 AttractedPosition;

        public AttractedMonsterEventArgs(Vector3 attractedPosition)
        {
            AttractedPosition = attractedPosition;
        }
    }
}
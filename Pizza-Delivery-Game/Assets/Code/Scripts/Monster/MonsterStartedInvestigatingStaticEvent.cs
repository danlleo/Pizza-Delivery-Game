using System;

namespace Monster
{
    public static class MonsterStartedInvestigatingStaticEvent
    {
        public static event EventHandler OnAnyMonsterStartedInvestigating;

        public static void CallMonsterStartedInvestigatingStaticEvent(this Monster monster)
        {
            OnAnyMonsterStartedInvestigating?.Invoke(monster, EventArgs.Empty);
        }
    }
}
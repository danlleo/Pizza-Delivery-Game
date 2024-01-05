using System;

namespace Monster
{
    public static class MonsterStoppedInvestigatingStaticEvent
    {
        public static event EventHandler OnAnyMonsterStoppedInvestigating;

        public static void CallMonsterStoppedInvestigatingStaticEvent(this Monster monster)
        {
            OnAnyMonsterStoppedInvestigating?.Invoke(monster, EventArgs.Empty);
        }
    }
}
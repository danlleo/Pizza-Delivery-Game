using System;

namespace Monster
{
    public static class MonsterStoppedInvestigatingStaticEvent
    {
        public static event EventHandler<MonsterStoppedInvestigatingEventArgs> OnAnyMonsterStoppedInvestigating;

        public static void CallMonsterStoppedInvestigatingStaticEvent(this Monster monster, MonsterStoppedInvestigatingEventArgs monsterStoppedInvestigatingEventArgs)
        {
            OnAnyMonsterStoppedInvestigating?.Invoke(monster, monsterStoppedInvestigatingEventArgs);
        }
    }

    public class MonsterStoppedInvestigatingEventArgs : EventArgs
    {
        public readonly bool HasDetectedPlayer;

        public MonsterStoppedInvestigatingEventArgs(bool hasDetectedPlayer)
        {
            HasDetectedPlayer = hasDetectedPlayer;
        }
    }
}
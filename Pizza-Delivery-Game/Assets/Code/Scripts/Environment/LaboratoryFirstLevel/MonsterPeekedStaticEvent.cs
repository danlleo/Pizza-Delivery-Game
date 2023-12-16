using System;
using Monster;

namespace Environment.LaboratoryFirstLevel
{
    public static class MonsterPeekedStaticEvent
    {
        public static event EventHandler OnAnyMonsterPeaked;
        
        public static void CallMonsterPeakedStaticEvent(this MonsterCornerPeek monsterCornerPeek)
            => OnAnyMonsterPeaked?.Invoke(monsterCornerPeek, EventArgs.Empty);
    }
}
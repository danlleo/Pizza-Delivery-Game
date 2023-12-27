using EventBus;

namespace Environment.LaboratoryFirstLevel
{
    public struct FixPipesEvent : IEvent
    {
        public readonly bool HasFixed;

        public FixPipesEvent(bool hasFixed)
        {
            HasFixed = hasFixed;
        }
    }
}
using System;

namespace EventBus
{
    internal interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventEmptyArgs { get; set; }
    }
    
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> _onEvent = _ => { };
        private Action _onEventEmptyArgs = () => { };
        
        Action<T> IEventBinding<T>.OnEvent
        {
            get => _onEvent;
            set => _onEvent = value;
        }

        Action IEventBinding<T>.OnEventEmptyArgs
        {
            get => _onEventEmptyArgs;
            set => _onEventEmptyArgs = value;
        }

        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;
        public EventBinding(Action onEventEmptyArgs) => _onEventEmptyArgs = _onEventEmptyArgs;

        public void Add(Action onEvent) => _onEventEmptyArgs += onEvent;
        public void Remove(Action onEvent) => _onEventEmptyArgs -= onEvent;
        
        public void Add(Action<T> onEvent) => _onEvent += onEvent;
        public void Remove(Action<T> onEvent) => _onEvent -= onEvent;
    }
}
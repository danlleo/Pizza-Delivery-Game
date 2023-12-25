using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> s_bindings = new();
        public static void Register(EventBinding<T> binding) => s_bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => s_bindings.Remove(binding);

        public static void Raise(T @event)
        {
            foreach (IEventBinding<T> binding in s_bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventEmptyArgs.Invoke();
            }
        }

        private static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            s_bindings.Clear();
        }
    }
}
using System;
using EventBus;
using UnityEngine;
using Utilities;

namespace Environment.LaboratoryFirstLevel
{
    [RequireComponent(typeof(BoxCollider))]
    [DisallowMultipleComponent]
    public class PipesBlockArea : MonoBehaviour
    {
        private BoxCollider _boxCollider;

        private EventBinding<FixPipesEvent> _fixPipesEventBinding;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.Enable();
        }

        private void OnEnable()
        {
            _fixPipesEventBinding = new EventBinding<FixPipesEvent>(HandleFixPipesEvent);
            EventBus<FixPipesEvent>.Register(_fixPipesEventBinding);
        }
        
        private void OnDisable()
        {
            EventBus<FixPipesEvent>.Deregister(_fixPipesEventBinding);
        }

        private void HandleFixPipesEvent(FixPipesEvent fixPipesEvent)
        {
            if (!fixPipesEvent.HasFixed) return;

            _boxCollider.Disable();
        }
    }
}

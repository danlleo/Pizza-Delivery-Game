using Enums.Player;
using UI.InspectableObject;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(StaminaEvent))]
    [RequireComponent(typeof(MovementEvent))]
    [RequireComponent(typeof(HoveringOverInteractableEvent))]
    [RequireComponent(typeof(StepEvent))]
    [RequireComponent(typeof(LandedEvent))]
    [RequireComponent(typeof(InspectableObjectFinishedReadingEvent))]
    [RequireComponent(typeof(InspectableObjectClosingEvent))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        [HideInInspector] public StaminaEvent StaminaEvent;
        [HideInInspector] public MovementEvent MovementEvent;
        [HideInInspector] public HoveringOverInteractableEvent HoveringOverInteractableEvent;
        [HideInInspector] public StepEvent StepEvent;
        [HideInInspector] public LandedEvent LandedEvent;
        [HideInInspector] public InspectableObjectFinishedReadingEvent InspectableObjectFinishedReadingEvent;
        [HideInInspector] public InspectableObjectClosingEvent InspectableObjectClosingEvent;
        
        private PlayerState _state;
        
        public void Awake()
        {
            StaminaEvent = GetComponent<StaminaEvent>();
            MovementEvent = GetComponent<MovementEvent>();
            HoveringOverInteractableEvent = GetComponent<HoveringOverInteractableEvent>();
            StepEvent = GetComponent<StepEvent>();
            LandedEvent = GetComponent<LandedEvent>();
            InspectableObjectFinishedReadingEvent = GetComponent<InspectableObjectFinishedReadingEvent>();
            InspectableObjectClosingEvent = GetComponent<InspectableObjectClosingEvent>();
            
            SetExploringState();
        }

        public void SetExploringState()
            => _state = PlayerState.Exploring;

        public void SetInspectingState()
            => _state = PlayerState.Inspecting;
        
        public PlayerState GetCurrentState()
            => _state;
    }
}

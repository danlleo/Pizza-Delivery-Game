using System;
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
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        [SerializeField] private UI.UI _ui;
        
        [HideInInspector] public StaminaEvent StaminaEvent;
        [HideInInspector] public MovementEvent MovementEvent;
        [HideInInspector] public HoveringOverInteractableEvent HoveringOverInteractableEvent;
        [HideInInspector] public StepEvent StepEvent;
        [HideInInspector] public LandedEvent LandedEvent;
        
        public PlayerState State { get; private set; }
        
        public void Awake()
        {
            StaminaEvent = GetComponent<StaminaEvent>();
            MovementEvent = GetComponent<MovementEvent>();
            HoveringOverInteractableEvent = GetComponent<HoveringOverInteractableEvent>();
            StepEvent = GetComponent<StepEvent>();
            LandedEvent = GetComponent<LandedEvent>();
            
            SetExploringState();
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
            _ui.InspectableObjectCloseEvent.Event += InspectableObjectClose_Event;
        }
        
        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
            _ui.InspectableObjectCloseEvent.Event -= InspectableObjectClose_Event;
        }
        
        private void SetExploringState()
            => State = PlayerState.Exploring;

        private void SetInspectingState()
            => State = PlayerState.Inspecting;
        
        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            SetInspectingState();
        }
        
        private void InspectableObjectClose_Event(object sender, EventArgs e)
        {
            SetExploringState();
        }
    }
}

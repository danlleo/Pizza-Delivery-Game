using System;
using Enums.Player;
using Environment.Bedroom.PC;
using Misc;
using Player.Inventory;
using UI.InspectableObject;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(StaminaEvent))]
    [RequireComponent(typeof(MovementEvent))]
    [RequireComponent(typeof(HoveringOverInteractableEvent))]
    [RequireComponent(typeof(StepEvent))]
    [RequireComponent(typeof(LandedEvent))]
    [RequireComponent(typeof(AddingItemEvent))]
    [RequireComponent(typeof(RemovingItemEvent))]
    [DisallowMultipleComponent]
    public class Player : Singleton<Player>
    {
        [SerializeField] private UI.UI _ui;
        
        [HideInInspector] public StaminaEvent StaminaEvent;
        [HideInInspector] public MovementEvent MovementEvent;
        [HideInInspector] public HoveringOverInteractableEvent HoveringOverInteractableEvent;
        [HideInInspector] public StepEvent StepEvent;
        [HideInInspector] public LandedEvent LandedEvent;
        [HideInInspector] public AddingItemEvent AddingItemEvent;
        [HideInInspector] public RemovingItemEvent RemovingItemEvent;
        
        public PlayerState State { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            StaminaEvent = GetComponent<StaminaEvent>();
            MovementEvent = GetComponent<MovementEvent>();
            HoveringOverInteractableEvent = GetComponent<HoveringOverInteractableEvent>();
            StepEvent = GetComponent<StepEvent>();
            LandedEvent = GetComponent<LandedEvent>();
            AddingItemEvent = GetComponent<AddingItemEvent>();
            RemovingItemEvent = GetComponent<RemovingItemEvent>();
            
            SetExploringState();
        }

        private void OnEnable()
        {
            _ui.InspectableObjectOpeningEvent.Event += InspectableObjectOpening_Event;
            _ui.InspectableObjectCloseEvent.Event += InspectableObjectClose_Event;
            StartedUsingPCStaticEvent.OnStarted += StartedUsingPCStaticEvent_OnStarted;
            StoppedUsingPCStaticEvent.OnEnded += StoppedUsingPCStaticEventOn_Ended;
        }

        private void OnDisable()
        {
            _ui.InspectableObjectOpeningEvent.Event -= InspectableObjectOpening_Event;
            _ui.InspectableObjectCloseEvent.Event -= InspectableObjectClose_Event;
            StartedUsingPCStaticEvent.OnStarted -= StartedUsingPCStaticEvent_OnStarted;
            StoppedUsingPCStaticEvent.OnEnded -= StoppedUsingPCStaticEventOn_Ended;
        }
        
        private void SetExploringState()
            => State = PlayerState.Exploring;

        private void SetInspectingState()
            => State = PlayerState.Inspecting;

        private void SetUsingPCState()
            => State = PlayerState.UsingPC;

        #region Events

        private void InspectableObjectOpening_Event(object sender, InspectableObjectOpeningEventArgs e)
        {
            SetInspectingState();
        }
        
        private void InspectableObjectClose_Event(object sender, EventArgs e)
        {
            SetExploringState();
        }
        private void StartedUsingPCStaticEvent_OnStarted(object sender, EventArgs e)
        {
            SetUsingPCState();
        }

        private void StoppedUsingPCStaticEventOn_Ended(object sender, EventArgs e)
        {
            SetExploringState();
        }
        
        #endregion
    }
}

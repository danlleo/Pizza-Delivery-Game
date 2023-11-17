using System;
using Enums.Scientist;
using Scientist.StateMachine;
using UnityEngine;

namespace Scientist
{
    [RequireComponent(typeof(InteractedWithScientistEvent))]
    [RequireComponent(typeof(StartedTalkingEvent))]
    [RequireComponent(typeof(StartedWalkingEvent))]
    [RequireComponent(typeof(StoppedWalkingEvent))]
    [RequireComponent(typeof(StartedOpeningDoorEvent))]
    [RequireComponent(typeof(OpenedDoorEvent))]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Scientist : MonoBehaviour
    {
        [HideInInspector] public InteractedWithScientistEvent InteractedWithScientistEvent;
        [HideInInspector] public StartedTalkingEvent StartedTalkingEvent;
        [HideInInspector] public StartedWalkingEvent StartedWalkingEvent;
        [HideInInspector] public StoppedWalkingEvent StoppedWalkingEvent;
        [HideInInspector] public StartedOpeningDoorEvent StartedOpeningDoorEvent;
        [HideInInspector] public OpenedDoorEvent OpenedDoorEvent;
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        public Transform endWalkingPointTransform => _endWalkingPointTransform;
        public ScientistType ScientistType => _scientistType;
        
        [Header("External references")]
        [SerializeField] private Transform _endWalkingPointTransform;
        
        [Header("Settings")] 
        [SerializeField] private ScientistType _scientistType;
        
        private void Awake()
        {
            InteractedWithScientistEvent = GetComponent<InteractedWithScientistEvent>();
            StartedTalkingEvent = GetComponent<StartedTalkingEvent>();
            StartedWalkingEvent = GetComponent<StartedWalkingEvent>();
            StoppedWalkingEvent = GetComponent<StoppedWalkingEvent>();
            StartedOpeningDoorEvent = GetComponent<StartedOpeningDoorEvent>();
            OpenedDoorEvent = GetComponent<OpenedDoorEvent>();
            
            StateMachine = new StateMachine.StateMachine();
            StateFactory = new StateFactory(this, StateMachine);
        }

        private void Start()
        {
            switch (_scientistType)
            {
                case ScientistType.Outdoor:
                    StateMachine.Initialize(StateFactory.Idle());
                    break;
                case ScientistType.LaboratoryEntry:
                    StateMachine.Initialize(StateFactory.Idle());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }

        public void DestroySelf()
            => Destroy(gameObject);
    }
}

using Scientist.Outdoor.StateMachine;
using UnityEngine;

namespace Scientist.Outdoor
{
    [RequireComponent(typeof(InteractedWithScientistEvent))]
    [RequireComponent(typeof(StartedTalkingEvent))]
    [RequireComponent(typeof(StartedWalkingEvent))]
    [RequireComponent(typeof(StoppedWalkingEvent))]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Scientist : MonoBehaviour
    {
        [HideInInspector] public InteractedWithScientistEvent InteractedWithScientistEvent;
        [HideInInspector] public StartedTalkingEvent StartedTalkingEvent;
        [HideInInspector] public StartedWalkingEvent StartedWalkingEvent;
        [HideInInspector] public StoppedWalkingEvent StoppedWalkingEvent;
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        public Transform CarTransform => _carTransform;

        [SerializeField] private Transform _carTransform;
        
        private void Awake()
        {
            InteractedWithScientistEvent = GetComponent<InteractedWithScientistEvent>();
            StartedTalkingEvent = GetComponent<StartedTalkingEvent>();
            StartedWalkingEvent = GetComponent<StartedWalkingEvent>();
            StoppedWalkingEvent = GetComponent<StoppedWalkingEvent>();
            
            StateMachine = new StateMachine.StateMachine();
            StateFactory = new StateFactory(this, StateMachine);
        }

        private void Start()
        {
            StateMachine.Initialize(StateFactory.Idle());
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }

        public void DestroySelf()
            => Destroy(gameObject);
    }
}

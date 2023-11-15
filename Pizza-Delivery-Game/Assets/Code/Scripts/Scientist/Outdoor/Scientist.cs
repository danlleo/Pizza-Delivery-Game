using Scientist.Outdoor.StateMachine;
using UnityEngine;

namespace Scientist.Outdoor
{
    [RequireComponent(typeof(InteractedWithScientistEvent))]
    [RequireComponent(typeof(StartedTalkingEvent))]
    [DisallowMultipleComponent]
    public class Scientist : MonoBehaviour
    {
        [HideInInspector] public InteractedWithScientistEvent InteractedWithScientistEvent;
        [HideInInspector] public StartedTalkingEvent StartedTalkingEvent; 
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        public Transform CarTransform => _carTransform;

        [SerializeField] private Transform _carTransform;
        
        private void Awake()
        {
            InteractedWithScientistEvent = GetComponent<InteractedWithScientistEvent>();
            StartedTalkingEvent = GetComponent<StartedTalkingEvent>();
            
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
    }
}

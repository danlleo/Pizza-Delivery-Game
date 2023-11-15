using Scientist.StateMachine;
using UnityEngine;

namespace Scientist
{
    [RequireComponent(typeof(InteractedWithScientistEvent))]
    [DisallowMultipleComponent]
    public class Scientist : MonoBehaviour
    {
        [HideInInspector] public InteractedWithScientistEvent InteractedWithScientistEvent;
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        private void Awake()
        {
            InteractedWithScientistEvent = GetComponent<InteractedWithScientistEvent>();
            
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

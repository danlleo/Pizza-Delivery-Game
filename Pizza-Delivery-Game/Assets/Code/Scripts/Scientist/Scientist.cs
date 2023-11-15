using Scientist.StateMachine;
using UnityEngine;

namespace Scientist
{
    public class Scientist : MonoBehaviour
    {
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        private void Awake()
        {
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

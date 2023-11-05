using System;
using Scientist.StateMachine.ConcreteStates;
using UnityEngine;
using IdleState = Player.StateMachine.ConcreteStates.IdleState;
using WalkingState = Player.StateMachine.ConcreteStates.WalkingState;

namespace Scientist
{
    public class Scientist : MonoBehaviour
    {
        public StateMachine.StateMachine StateMachine { get; set; }
        
        public StateMachine.ConcreteStates.IdleState IdleState { get; set; }
        public StateMachine.ConcreteStates.WalkingState WalkingState { get; set; }

        private void Awake()
        {
            StateMachine = new StateMachine.StateMachine();

            //IdleState = new IdleState(this, StateMachine);
            //WalkingState = new WalkingState(this, StateMachine);
        }

        private void Start()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }
    }
}

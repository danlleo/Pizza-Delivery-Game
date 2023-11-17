using Monster.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(StartedChasingEvent))]
    [RequireComponent(typeof(StoppedChasingEvent))]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Monster : MonoBehaviour
    {
        [HideInInspector] public StartedChasingEvent StartedChasingEvent;
        [HideInInspector] public StoppedChasingEvent StoppedChasingEvent;
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        public NavMeshAgent NavMeshAgent { get; private set; }

        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _runningSpeed;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            StartedChasingEvent = GetComponent<StartedChasingEvent>();
            StoppedChasingEvent = GetComponent<StoppedChasingEvent>();
            
            StateMachine = new StateMachine.StateMachine();
            StateFactory = new StateFactory(this, StateMachine);
        }

        private void Start()
        {
            StateMachine.Initialize(StateFactory.Chase());
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }
    }
}

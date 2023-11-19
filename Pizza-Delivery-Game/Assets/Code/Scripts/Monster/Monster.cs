using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Monster.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Monster
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(FieldOfView))]
    [RequireComponent(typeof(StartedChasingEvent))]
    [RequireComponent(typeof(StoppedChasingEvent))]
    [RequireComponent(typeof(DetectedTargetEvent))]
    [RequireComponent(typeof(LostTargetEvent))]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Monster : MonoBehaviour
    {
        [HideInInspector] public StartedChasingEvent StartedChasingEvent;
        [HideInInspector] public StoppedChasingEvent StoppedChasingEvent;
        [HideInInspector] public DetectedTargetEvent DetectedTargetEvent;
        [HideInInspector] public LostTargetEvent LostTargetEvent;
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;

        public NavMeshAgent NavMeshAgent { get; private set; }
        public FieldOfView FieldOfView { get; private set; }
        public IEnumerable<Transform> PatrolPointList => new ReadOnlyCollection<Transform>(_patrolPointList);

        [Header("External references")] 
        [SerializeField] private List<Transform> _patrolPointList = new();
        
        [Header("Settings")]
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _runningSpeed;
        
        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            FieldOfView = GetComponent<FieldOfView>();
            StartedChasingEvent = GetComponent<StartedChasingEvent>();
            StoppedChasingEvent = GetComponent<StoppedChasingEvent>();
            DetectedTargetEvent = GetComponent<DetectedTargetEvent>();
            LostTargetEvent = GetComponent<LostTargetEvent>();

            FieldOfView.enabled = false;
            NavMeshAgent.speed = _walkingSpeed;
            
            StateMachine = new StateMachine.StateMachine();
            StateFactory = new StateFactory(this, StateMachine);
        }

        private void Start()
        {
            StateMachine.Initialize(StateFactory.Roam());
        }

        private void OnEnable()
        {
            StartedChasingEvent.Event += StartedChasing_Event;
            StoppedChasingEvent.Event += StoppedChasing_Event;
        }

        private void OnDisable()
        {
            StartedChasingEvent.Event -= StartedChasing_Event;
            StoppedChasingEvent.Event -= StoppedChasing_Event;
        }

        private void StartedChasing_Event(object sender, EventArgs e)
        {
            NavMeshAgent.speed = _runningSpeed;
        }

        private void StoppedChasing_Event(object sender, EventArgs e)
        {
            NavMeshAgent.speed = _walkingSpeed;
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }
    }
}

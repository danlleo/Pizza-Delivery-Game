using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Environment.LaboratorySecondLevel;
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
    [RequireComponent(typeof(StartedPatrollingEvent))]
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Monster : MonoBehaviour
    {
        [HideInInspector] public StartedChasingEvent StartedChasingEvent;
        [HideInInspector] public StoppedChasingEvent StoppedChasingEvent;
        [HideInInspector] public DetectedTargetEvent DetectedTargetEvent;
        [HideInInspector] public LostTargetEvent LostTargetEvent;
        [HideInInspector] public StartedPatrollingEvent StartedPatrollingEvent;
        
        public StateMachine.StateMachine StateMachine { get; set; }
        public StateFactory StateFactory;
        
        public NavMeshAgent NavMeshAgent { get; private set; }
        public FieldOfView FieldOfView { get; private set; }
        public IEnumerable<Transform> PatrolPointList => new ReadOnlyCollection<Transform>(_patrolPointList);

        public Vector3 InvestigatePosition { get; private set; }
        
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
            StartedPatrollingEvent = GetComponent<StartedPatrollingEvent>();
            
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
            AttractedMonsterStaticEvent.OnAnyAttractedMonster += Monster_OnAnyAttractedMonster;
        }
        
        private void OnDisable()
        {
            StartedChasingEvent.Event -= StartedChasing_Event;
            StoppedChasingEvent.Event -= StoppedChasing_Event;
            AttractedMonsterStaticEvent.OnAnyAttractedMonster -= Monster_OnAnyAttractedMonster;
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }
        
        private void StartedChasing_Event(object sender, EventArgs e)
        {
            NavMeshAgent.speed = _runningSpeed;
        }

        private void StoppedChasing_Event(object sender, EventArgs e)
        {
            NavMeshAgent.speed = _walkingSpeed;
        }
        
        private void Monster_OnAnyAttractedMonster(object sender, AttractedMonsterEventArgs e)
        {
            InvestigatePosition = e.AttractedPosition;
        }
    }
}

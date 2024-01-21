using Cinemachine;
using DataPersistence;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(StaminaEvent))]
    [RequireComponent(typeof(MovementEvent))]
    [RequireComponent(typeof(HoveringOverInteractableEvent))]
    [RequireComponent(typeof(StepEvent))]
    [RequireComponent(typeof(LandedEvent))]
    [RequireComponent(typeof(SprintStateChangedEvent))]
    [DisallowMultipleComponent]
    public sealed class Player : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _itemHolderTransform;
        
        [HideInInspector] public StaminaEvent StaminaEvent;
        [HideInInspector] public MovementEvent MovementEvent;
        [HideInInspector] public HoveringOverInteractableEvent HoveringOverInteractableEvent;
        [HideInInspector] public StepEvent StepEvent;
        [HideInInspector] public LandedEvent LandedEvent;
        
        [HideInInspector] public SprintStateChangedEvent SprintStateChangedEvent;
        
        [field:SerializeField] public CinemachineVirtualCamera MainVirtualCamera { get; private set; }
        [field:SerializeField] public Camera UICamera { get; private set; }
        
        public global::Inventory.Inventory Inventory { get; private set; }

        private void Awake()
        {
            StaminaEvent = GetComponent<StaminaEvent>();
            MovementEvent = GetComponent<MovementEvent>();
            HoveringOverInteractableEvent = GetComponent<HoveringOverInteractableEvent>();
            StepEvent = GetComponent<StepEvent>();
            LandedEvent = GetComponent<LandedEvent>();
            SprintStateChangedEvent = GetComponent<SprintStateChangedEvent>();
            Inventory = new global::Inventory.Inventory();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
                LoadStaticEvent.Call(this);
            
            if (Input.GetKeyDown(KeyCode.N))
                NewGameStaticEvent.Call(this);
                
            if (Input.GetKeyDown(KeyCode.Space))
                SaveStaticEvent.Call(this);
        }

        public Transform GetItemHolderTransform()
            => _itemHolderTransform;
    }
}

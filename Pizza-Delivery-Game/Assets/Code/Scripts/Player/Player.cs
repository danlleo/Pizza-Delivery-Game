using Cinemachine;
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

        public void PlaceAt(Vector3 targetPosition)
            => transform.position = targetPosition;

        public Transform GetItemHolderTransform()
            => _itemHolderTransform;
    }
}

using Misc;
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
    public sealed class Player : Singleton<Player>
    {
        [Header("External references")]
        [SerializeField] private Transform _itemHolderTransform;
        
        [HideInInspector] public StaminaEvent StaminaEvent;
        [HideInInspector] public MovementEvent MovementEvent;
        [HideInInspector] public HoveringOverInteractableEvent HoveringOverInteractableEvent;
        [HideInInspector] public StepEvent StepEvent;
        [HideInInspector] public LandedEvent LandedEvent;
        
        [HideInInspector] public SprintStateChangedEvent SprintStateChangedEvent;

        protected override void Awake()
        {
            base.Awake();
            
            StaminaEvent = GetComponent<StaminaEvent>();
            MovementEvent = GetComponent<MovementEvent>();
            HoveringOverInteractableEvent = GetComponent<HoveringOverInteractableEvent>();
            StepEvent = GetComponent<StepEvent>();
            LandedEvent = GetComponent<LandedEvent>();
            SprintStateChangedEvent = GetComponent<SprintStateChangedEvent>();
        }

        public void PlaceAt(Vector3 targetPosition)
            => transform.position = targetPosition;

        public Transform GetItemHolderTransform()
            => _itemHolderTransform;
    }
}

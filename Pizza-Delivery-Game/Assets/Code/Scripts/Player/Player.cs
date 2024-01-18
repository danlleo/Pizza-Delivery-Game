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

        /// <summary>
        /// I'm learning about dependency inversion, and I'm sure what I did here is horrible,
        /// but when I will learn zenject, I will understand how to get rid of this tightly coupled code.
        /// </summary>
        public global::Inventory.Inventory Inventory { get; private set; }
        
        public void Initialize()
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

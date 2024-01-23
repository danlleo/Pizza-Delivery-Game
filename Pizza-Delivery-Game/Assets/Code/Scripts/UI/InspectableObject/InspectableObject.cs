using InspectableObject;
using Interfaces;
using UnityEngine;
using Zenject;

namespace UI.InspectableObject
{
    public abstract class InspectableObject : WorldScreenSpaceIcon.WorldScreenSpaceIcon, IInteractable
    {
        protected abstract InspectableObjectSO InspectableObjectSO { get; }
        protected abstract string ActionDescription { get; }

        private Player.Player _player;
        private InspectableObjectTrigger _inspectableObjectTrigger;
        
        [Inject]
        private void Construct(Player.Player player, InspectableObjectTrigger inspectableObjectTrigger)
        {
            _player = player;
            _inspectableObjectTrigger = inspectableObjectTrigger;
        }
        
        public virtual void Interact()
        {
            _inspectableObjectTrigger.Invoke(InspectableObjectSO, AddToInventory);
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return ActionDescription;
        }

        private void AddToInventory()
        {
            if (!_player.Inventory.TryAddItem(InspectableObjectSO.Item, out bool _))
                Debug.LogError("Error when adding item to the inventory");
        }
    }
}
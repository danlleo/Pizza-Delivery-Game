using InspectableObject;
using Interfaces;
using UnityEngine;
using Player.Inventory;
using WorldScreenSpaceIcon;

namespace Environment.LaboratoryFirstLevel
{
    public class Wrench : WorldScreenSpaceIcon.WorldScreenSpaceIcon, IInteractable, IInspectable
    {
        [SerializeField] private InspectableObjectSO _wrenchInspectableObject;
        
        public void Interact()
        {
            Trigger.Instance.Invoke(_wrenchInspectableObject, AddToInventory);
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Wrench";
        }

        public override WorldScreenSpaceIconData GetWorldScreenSpaceIconData()
        {
            return new WorldScreenSpaceIconData(transform, Vector3.zero);
        }

        public void AddToInventory()
        {
            OnAnyAddedItemEvent.Call(this, new AddingItemEventArgs(_wrenchInspectableObject.Item));
        }
    }
}

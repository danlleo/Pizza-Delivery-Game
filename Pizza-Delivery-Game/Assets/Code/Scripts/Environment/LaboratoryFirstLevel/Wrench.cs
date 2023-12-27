using InspectableObject;
using Interfaces;
using UI;
using UnityEngine;
using Player.Inventory;

namespace Environment.LaboratoryFirstLevel
{
    public class Wrench : MonoBehaviour, IInteractable, IWorldScreenSpaceIcon, IInspectable
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

        public WorldScreenSpaceIcon GetWorldScreenSpaceIcon()
        {
            return new WorldScreenSpaceIcon(transform, Vector3.zero);
        }

        public void AddToInventory()
        {
            Player.Player player = Player.Player.Instance;
            player.AddingItemEvent.Call(player, new AddingItemEventArgs(_wrenchInspectableObject.Item));
        }
    }
}

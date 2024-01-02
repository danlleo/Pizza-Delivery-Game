using InspectableObject;
using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Flashlight : MonoBehaviour, IInteractable, IInspectable
    {
        [Header("External references")]
        [SerializeField] private InspectableObjectSO _inspectableObject;
        
        public void Interact()
        {
            Trigger.Instance.Invoke(_inspectableObject, AddToInventory);
            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Flashlight";
        }

        public void AddToInventory()
        {
            Player.Player player = Player.Player.Instance;
            player.AddingItemEvent.Call(player, new AddingItemEventArgs(_inspectableObject.Item));
        }
    }
}

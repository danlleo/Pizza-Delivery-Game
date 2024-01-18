using InspectableObject;
using Interfaces;
using UnityEngine;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Flashlight : MonoBehaviour, IInteractable, IInspectable
    {
        [Header("External references")] 
        [SerializeField] private Player.Player _player;
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
            if (!_player.Inventory.TryAddItem(_inspectableObject.Item, out bool _))
                Debug.LogError("Error when adding item to the inventory");
        }
    }
}

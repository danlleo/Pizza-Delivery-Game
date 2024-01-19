using InspectableObject;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Environment.LaboratoryFirstLevel
{
    [DisallowMultipleComponent]
    public class Flashlight : MonoBehaviour, IInteractable, IInspectable
    {
        [SerializeField] private InspectableObjectSO _inspectableObject;
        
        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
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

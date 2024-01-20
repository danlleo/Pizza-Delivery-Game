using InspectableObject;
using Interfaces;
using UnityEngine;
using WorldScreenSpaceIcon;
using Zenject;

namespace Environment.LaboratoryFirstLevel
{
    public class Wrench : WorldScreenSpaceIcon.WorldScreenSpaceIcon, IInteractable, IInspectable
    {
        [SerializeField] private InspectableObjectSO _wrenchInspectableObject;

        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        public void Interact()
        {
            InspectableObjectTrigger.Instance.Invoke(_wrenchInspectableObject, AddToInventory);
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
            _player.Inventory.TryAddItem(_wrenchInspectableObject.Item, out bool _);
        }
    }
}

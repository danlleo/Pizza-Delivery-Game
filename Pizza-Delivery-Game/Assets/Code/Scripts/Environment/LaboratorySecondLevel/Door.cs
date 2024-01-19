using Interfaces;
using Player.Inventory;
using UnityEngine;
using Zenject;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private ItemSO _pizzaBox;

        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        public void Interact()
        {
            if (!_player.Inventory.HasItem(_pizzaBox))
            {
                NoPizzaBoxStaticEvent.Call(this);
                return;
            }
            
            // TODO: implement action
        }

        public string GetActionDescription()
        {
            return "Door";
        }
    }
}

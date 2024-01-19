using Interfaces;
using Player.Inventory;
using UnityEngine;
using Zenject;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class PizzaBox : MonoBehaviour, IInteractable
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
            _player.Inventory.TryAddItem(_pizzaBox, out bool _);

            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Pizza box";
        }
    }
}

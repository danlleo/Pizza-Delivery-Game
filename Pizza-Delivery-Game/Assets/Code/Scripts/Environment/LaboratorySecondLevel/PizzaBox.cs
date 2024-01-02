using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class PizzaBox : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private ItemSO _pizzaBox;
        
        public void Interact()
        {
            Player.Player player =  Player.Player.Instance;

            player.AddingItemEvent.Call(player, new AddingItemEventArgs(_pizzaBox));

            Destroy(gameObject);
        }

        public string GetActionDescription()
        {
            return "Pizza box";
        }
    }
}

using System;
using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Environment.LaboratorySecondLevel
{
    [DisallowMultipleComponent]
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private ItemSO _pizzaBox;
        
        public void Interact()
        {
            if (!Player.Player.Instance.TryGetComponent(out Inventory.Inventory inventory))
            {
                throw new Exception(
                    "Player component doesn't have inventory component, or player reference null itself");
            }

            if (!inventory.HasItem(_pizzaBox))
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

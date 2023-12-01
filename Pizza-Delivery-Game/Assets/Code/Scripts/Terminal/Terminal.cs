using Interfaces;
using Player.Inventory;
using UnityEngine;

namespace Terminal
{
    [DisallowMultipleComponent]
    [SelectionBase]
    public class Terminal : MonoBehaviour, IInteractable
    {
        [Header("External references")]
        [SerializeField] private ItemSO _requiredKeycard;
        [SerializeField] private Door.Door _door;
        
        public void Interact()
        {
            print(Player.Player.Instance.GetComponent<Inventory>().HasItem(_requiredKeycard) ? "Opened" : "Can't open");
        }

        public string GetActionDescription()
        {
            return "Insert keycard";
        }
    }
}

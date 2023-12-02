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
            if (!Player.Player.Instance.GetComponent<Inventory>().HasItem(_requiredKeycard)) return;
            
            _door.Unlock();
            _door.Open();
            
            Destroy(this);
        }

        public string GetActionDescription()
        {
            return "Insert keycard";
        }
    }
}
